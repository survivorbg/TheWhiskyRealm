using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Event;
using static TheWhiskyRealm.Core.Constants.RoleConstants;
using static TheWhiskyRealm.Core.Constants.ControllerConstants;

namespace TheWhiskyRealm.Controllers;

public class EventController : BaseController
{
    private readonly IEventService eventService;
    private readonly IVenueService venueService;

    public EventController(IEventService eventService, IVenueService venueService)
    {
        this.eventService = eventService;
        this.venueService = venueService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = await eventService.GetAllEventsAsync();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> PastEvents()
    {
        var model = await eventService.GetAllPastEventsAsync();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var model = await eventService.GetEventAsync(id);
        
        if (model == null)
        {
            return NotFound();
        }
        model.JoinedUsers = await eventService.GetJoinedUsersAsync(id);

        return View(model);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await eventService.GetEventForEditAsync(id);
        if (model == null)
        {
            return NotFound();
        }

        if(User.Id() != await eventService.GetOrganiserIdAsync(id) && !User.IsAdmin())
        {
            return Unauthorized();
        }

        if(await eventService.HasAlreadyStartedAsync(id))
        {
            return BadRequest();
        }

        if(await venueService.VenueExistAsync(model.VenueId) == false)
        {
            return NotFound();
        }

        var alreadyJoined = await eventService.GetJoinedUsersCountAsync(id);
        model.Venues = await venueService.GetVenuesWithMoreCapacityAsync(alreadyJoined);

        return View(model);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpPost]
    public async Task<IActionResult> Edit(EventEditViewModel model)
    {
        var ev = await eventService.GetEventAsync(model.Id);
        if (ev == null)
        {
            return NotFound();
        }

        if (await eventService.HasAlreadyStartedAsync(model.Id))
        {
            return BadRequest();
        }

        if (User.Id() != await eventService.GetOrganiserIdAsync(model.Id) && !User.IsAdmin())
        {
            return Unauthorized();
        }

        if ( model.StartDate >= model.EndDate)
        {
            ModelState.AddModelError(nameof(model.EndDate), EndDateMustBeAfterStartDate);
        }

        if (model.StartDate <= DateTime.Now.AddDays(1))
        {
            ModelState.AddModelError(nameof(model.StartDate), EventMustStartAtLeastOneDayFromNow);
        }

        if (await venueService.VenueExistAsync(model.VenueId) == false)
        {
            ModelState.AddModelError(nameof(model.VenueId), MustChooseAValidVenue);
        }
        var alreadyJoined = await eventService.GetJoinedUsersCountAsync(model.Id);

        if (!ModelState.IsValid)
        {
            model.Venues = await venueService.GetVenuesWithMoreCapacityAsync(alreadyJoined);
            return View(model);
        }

        var availableSpots = await venueService.GetVenueCapacityAsync(model.VenueId) - alreadyJoined;
        await eventService.EditEventAsync(model, availableSpots);

        return RedirectToAction(nameof(Details), new {id=model.Id});
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpGet]
    public async Task<IActionResult> Add()
    {

        var model = new EventAddViewModel();
        model.Venues = await venueService.GetVenuesAsync();

        return View(model);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpPost]
    public async Task<IActionResult> Add(EventAddViewModel model)
    {
        if (model.StartDate >= model.EndDate)
        {
            ModelState.AddModelError(nameof(model.EndDate), EndDateMustBeAfterStartDate);
        }

        if (model.StartDate <= DateTime.Now.AddDays(1))
        {
            ModelState.AddModelError(nameof(model.StartDate), EventMustStartAtLeastOneDayFromNow);
        }

        if(await venueService.VenueExistAsync(model.VenueId) == false)
        {
            ModelState.AddModelError(nameof(model.VenueId), MustChooseAValidVenue);
        }

        if (!ModelState.IsValid)
        {
            model!.Venues = await venueService.GetVenuesAsync();
            return View(model);
        }

        var userId = User.Id();
        await eventService.AddEventAsync(model, userId);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = $"{Administrator},{UserRole}")]
    [HttpPost]
    public async Task<IActionResult> Join(int id)
    {
        if(await eventService.EventExistAsync(id) == false)
        {
            return NotFound();
        }

        var userId = User.Id();

        if(await eventService.IsUserAlreadyJoinedAsync(id, userId) || await eventService.GetOrganiserIdAsync(id) == userId)
        {
            return BadRequest();
        }

        if(await eventService.HasAvaialbleSpotsAsync(id) == false || await eventService.HasAlreadyStartedAsync(id))
        {
            return BadRequest();
        }

        await eventService.JoinEventAsync(id, userId);

        return RedirectToAction(nameof(MyEvents)); 
    }

    [Authorize(Roles = $"{Administrator},{UserRole}")]
    [HttpPost]
    public async Task<IActionResult> Leave(int id)
    {
        if (await eventService.EventExistAsync(id) == false)
        {
            return NotFound();
        }

        var userId = User.Id();

        if (await eventService.IsUserAlreadyJoinedAsync(id, userId) == false)
        {
            return BadRequest();
        }

        if (await eventService.HasAlreadyStartedAsync(id))
        {
            return BadRequest();
        }

        await eventService.LeaveEventAsync(id, userId);

        return RedirectToAction(nameof(MyEvents));
    }


    [HttpGet]
    public async Task<IActionResult> MyEvents()
    {
        var userId = User.Id();
        var model = await eventService.GetUserEventsAsync(userId);


        return View(model);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpGet]
    public async Task<IActionResult> OrganisedEvents()
    {
        var userId = User.Id();
        var model = await eventService.GetEventsOrganisedByUserAsync(userId);

        return View(model);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ev = await eventService.GetEventAsync(id);
        if (ev == null)
        {
            return NotFound();
        }

        if (await eventService.HasAlreadyStartedAsync(id))
        {
            return BadRequest();
        }

        if (User.Id() != await eventService.GetOrganiserIdAsync(id))
        {
            return Unauthorized();
        }

        await eventService.DeleteEventAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
