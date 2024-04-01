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
            ModelState.AddModelError("EndDate", "End date must be after start date.");
        }

        if (!ModelState.IsValid)
        {
            model!.Venues = await venueService.GetVenuesAsync();
            return View(model);
        }

        await eventService.EditEventAsync(model,startDate,endDate);

        return RedirectToAction("Details", new {id=model.Id});
    }

}
