using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Event;

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
        if(model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [Authorize(Roles = "Administrator, WhiskyExpert")]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var ev = await eventService.GetEventAsync(id);
        if (ev == null)
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

        var model = await eventService.GetEventForEditAsync(id);
        model!.Venues = await venueService.GetVenuesAsync();

        return View(model);
    }

    [Authorize(Roles = "Administrator, WhiskyExpert")]
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
            ModelState.AddModelError(nameof(model.EndDate), "End date must be after start date.");
        }

        if (model.StartDate <= DateTime.Now.AddDays(1))
        {
            ModelState.AddModelError(nameof(model.StartDate), "The event must start atleast one day from now.");
        }

        if (await venueService.VenueExistAsync(model.VenueId) == false)
        {
            ModelState.AddModelError(nameof(model.VenueId), "You must choose a valid venue.");
        }

        if (!ModelState.IsValid)
        {
            model!.Venues = await venueService.GetVenuesAsync();
            return View(model);
        }

        await eventService.EditEventAsync(model);

        return RedirectToAction("Details", new {id=model.Id});
    }

    [Authorize(Roles = "Administrator, WhiskyExpert")]
    [HttpGet]
    public async Task<IActionResult> Add()
    {

        var model = new EventAddViewModel();
        model.Venues = await venueService.GetVenuesAsync();

        return View(model);
    }

    [Authorize(Roles = "Administrator, WhiskyExpert")]
    [HttpPost]
    public async Task<IActionResult> Add(EventAddViewModel model)
    {
        if (model.StartDate >= model.EndDate)
        {
            ModelState.AddModelError(nameof(model.EndDate), "End date must be after start date.");
        }

        if (model.StartDate <= DateTime.Now.AddDays(1))
        {
            ModelState.AddModelError(nameof(model.StartDate), "The event must start atleast one day from now.");
        }

        if(await venueService.VenueExistAsync(model.VenueId) == false)
        {
            ModelState.AddModelError(nameof(model.VenueId), "You must choose a valid venue.");
        }

        if (!ModelState.IsValid)
        {
            model!.Venues = await venueService.GetVenuesAsync();
            return View(model);
        }

        var userId = User.Id();
        await eventService.AddEventAsync(model, userId);

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "WhiskyExpert, User")]
    [HttpPost]
    public async Task<IActionResult> Join(int id)
    {
        if(await eventService.EventExistAsync(id) == false)
        {
            return NotFound();
        }

        var userId = User.Id();
        if(userId == null)
        {
            return RedirectToPage("/Identity/Login"); //TODO Check 
        }

        if(await eventService.IsUserAlreadyJoinedAsync(id, userId) || await eventService.GetOrganiserIdAsync(id) == userId)
        {
            return BadRequest();
        }

        if(await eventService.HasAvaialbleSpotsAsync(id) == false || await eventService.HasAlreadyStartedAsync(id))
        {
            return BadRequest();
        }

        await eventService.JoinEventAsync(id, userId);

        return RedirectToAction("MyEvents"); 
    }

    [Authorize(Roles = "WhiskyExpert, User")]
    [HttpPost]
    public async Task<IActionResult> Leave(int id)
    {
        if (await eventService.EventExistAsync(id) == false)
        {
            return NotFound();
        }

        var userId = User.Id();
        if (userId == null)
        {
            return RedirectToPage("/Identity/Login"); //TODO Check 
        }

        if (await eventService.IsUserAlreadyJoinedAsync(id, userId) == false)
        {
            return BadRequest();
        }

        if (await eventService.HasAlreadyStartedAsync(id))
        {
            return BadRequest();
        }

        await eventService.LeaveEventAsync(id, userId);

        return RedirectToAction("MyEvents");
    }


    [HttpGet]
    public async Task<IActionResult> MyEvents()
    {
        var userId = User.Id();
        var model = await eventService.GetUserEventsAsync(userId);


        return View(model);
    }

    [Authorize(Roles = "Administrator, WhiskyExpert")]
    [HttpGet]
    public async Task<IActionResult> OrganisedEvents()
    {
        var userId = User.Id();
        var model = await eventService.GetEventsOrganisedByUserAsync(userId);

        return View(model);
    }

    [Authorize(Roles = "Administrator, WhiskyExpert")]
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

        return RedirectToAction("Index");
    }
}
