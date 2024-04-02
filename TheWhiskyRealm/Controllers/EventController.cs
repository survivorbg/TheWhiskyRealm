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
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var ev = await eventService.GetEventAsync(id);
        if (ev == null)
        {
            return NotFound();
        }

        if(User.Id() != await eventService.GetOrganiserIdAsync(id))
        {
            return Unauthorized();
        }

        var model = await eventService.GetEventForEditAsync(id);
        model!.Venues = await venueService.GetVenuesAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EventEditViewModel model)
    {
        var ev = await eventService.GetEventAsync(model.Id);
        if (ev == null)
        {
            return NotFound();
        }

        if (User.Id() != await eventService.GetOrganiserIdAsync(model.Id))
        {
            return Unauthorized();
        }

        DateTime startDate = DateTime.Now;

        if (!DateTime.TryParseExact(
           model.StartDate,
           "hh:mm dd.MM.yyyy",
           CultureInfo.InvariantCulture,
           DateTimeStyles.None,
           out startDate))
        {
            ModelState.AddModelError(nameof(model.StartDate), $"Invalid date! Format must be {"hh:mm dd.MM.yyyy"}");
        }

        DateTime endDate = DateTime.Now;

        if (!DateTime.TryParseExact(
           model.EndDate,
           "hh:mm dd.MM.yyyy",
           CultureInfo.InvariantCulture,
           DateTimeStyles.None,
           out endDate))
        {
            ModelState.AddModelError(nameof(model.EndDate), $"Invalid date! Format must be {"hh:mm dd.MM.yyyy"}");
        }

        if (startDate >= endDate)
        {
            ModelState.AddModelError(nameof(model.EndDate), "End date must be after start date.");
        }

        if (startDate <= DateTime.Now.AddDays(1))
        {
            ModelState.AddModelError((model.StartDate), "The event must start atleast one day from now.");
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

        await eventService.EditEventAsync(model,startDate,endDate);

        return RedirectToAction("Details", new {id=model.Id});
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {

        var model = new EventAddViewModel();
        model.Venues = await venueService.GetVenuesAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(EventAddViewModel model)
    {
        DateTime startDate = DateTime.Now;

        if (!DateTime.TryParseExact(
           model.StartDate,
           "hh:mm dd.MM.yyyy",
           CultureInfo.InvariantCulture,
           DateTimeStyles.None,
           out startDate))
        {
            ModelState.AddModelError(nameof(model.StartDate), $"Invalid date! Format must be {"hh:mm dd.MM.yyyy"}");
        }

        DateTime endDate = DateTime.Now;

        if (!DateTime.TryParseExact(
           model.EndDate,
           "hh:mm dd.MM.yyyy",
           CultureInfo.InvariantCulture,
           DateTimeStyles.None,
           out endDate))
        {
            ModelState.AddModelError(nameof(model.EndDate), $"Invalid date! Format must be {"hh:mm dd.MM.yyyy"}");
            model!.Venues = await venueService.GetVenuesAsync();
            return View(model);
        }

        if (startDate >= endDate)
        {
            ModelState.AddModelError(nameof(model.EndDate), "End date must be after start date.");
        }

        if (startDate <= DateTime.Now.AddDays(1))
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
        await eventService.AddEventAsync(model, startDate, endDate, userId);

        return RedirectToAction("Index");
    }
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

        if(await eventService.HasAvaialbleSpotsAsync(id) == false || await eventService.HasAlreadyStartedAsyn(id))
        {
            return BadRequest();
        }

        await eventService.JoinEventAsync(id, userId);

        return View("Index"); //TODO change to MyEvents
    }

}
