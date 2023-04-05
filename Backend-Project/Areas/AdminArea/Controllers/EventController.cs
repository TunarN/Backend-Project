using Backend_Project.DAL;
using Backend_Project.Extension;
using Backend_Project.Models;
using Backend_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Backend_Project.Areas.AdminArea.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;
        public EventController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }

        public IActionResult Index()
        {
            EventVM eventVM = new();
            eventVM.Events = _appDbContext.Events.ToList();
            return View(eventVM);
        }

        public IActionResult Details(int id) 
        {
            if(id==null) return NotFound();
            Event events=_appDbContext.Events.Include(e=>e.EventSpeakers).ThenInclude(e=>e.Speakar).FirstOrDefault(e=>e.Id==id);
            if(events==null) return NotFound();
            return View(new EventDetailVM {Name=events.EventName , EventDateTime = events.EventDateTime, StartTime= events.StartTime,EndTime=events.EndTime,EventDesc=events.EventDesc,EventSpeakers=events.EventSpeakers,EventImageUrl=events.ImageUrl});
        }

        public IActionResult Create()
        {
            ViewBag.Speakers = new SelectList(_appDbContext.Speakers.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateVM eventCreateVM)
        {
            ViewBag.Speakers = new SelectList(_appDbContext.Speakers.ToList(), "Id", "Name");
            Event newevents = new();
            newevents.EventName = eventCreateVM.Name;
            newevents.EventDesc = eventCreateVM.EventDesc;
            newevents.EventDateTime = eventCreateVM.EventDateTime;
            newevents.StartTime = eventCreateVM.StartTime;
            newevents.EndTime= eventCreateVM.EndTime;
            newevents.Venue = eventCreateVM.Venue;
            List<EventSpeaker> speakers = new();
            foreach (var item in eventCreateVM.SpeakerId)
            {
                EventSpeaker eventSpeaker = new();
                eventSpeaker.EventId = newevents.Id;
                eventSpeaker.SpeakerId = item;
                _appDbContext.EventSpeakers.Add(eventSpeaker);
                speakers.Add(eventSpeaker);
            }

            if (!eventCreateVM.Image.IsImage())
            {
                ModelState.AddModelError("Img", "only img");
                return View();
            }

            newevents.ImageUrl = eventCreateVM.Image.SaveImage(_env, "img/event", eventCreateVM.Image.FileName);
            newevents.EventSpeakers=speakers;
            _appDbContext.Events.Add(newevents);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (id == null) return NotFound();
            Event events = _appDbContext.Events.Include(e => e.EventSpeakers).ThenInclude(e => e.Speakar).FirstOrDefault(e => e.Id == id);
            if (events == null) return NotFound();
            ViewBag.Speakers = new SelectList(_appDbContext.Speakers.ToList(), "Id", "Name");
            List<Speaker> speakers = new();
            foreach (var item in events.EventSpeakers)
            {
                Speaker speaker = _appDbContext.Speakers.FirstOrDefault(s => s.Id == item.SpeakerId);
                speakers.Add(speaker);
            }
            ViewBag.ExistSpeakers = new SelectList(speakers, "Id", "Name");
            return View(new EventEditVM { Name = events.EventName, EventDateTime = events.EventDateTime, StartTime = events.StartTime, EndTime = events.EndTime, EventDesc = events.EventDesc, EventSpeakers = events.EventSpeakers });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EventEditVM eventEditVM)
        {
            if (id == null) return NotFound();
            Event events = _appDbContext.Events.Include(e => e.EventSpeakers).ThenInclude(e => e.Speakar).FirstOrDefault(e => e.Id == id);
            if (events == null) return NotFound();
            ViewBag.Speakers = new SelectList(_appDbContext.Speakers.ToList(), "Id", "Name");
            List<Speaker> speakers = new();
            foreach (var item in events.EventSpeakers)
            {
                Speaker speaker = _appDbContext.Speakers.FirstOrDefault(s => s.Id == item.SpeakerId);
                speakers.Add(speaker);
            }
            events.EventName = eventEditVM.Name;
            events.EventDesc = eventEditVM.EventDesc;
            events.EventDateTime = eventEditVM.EventDateTime;
            events.Venue= eventEditVM.Venue;
            if (eventEditVM.StartTime !=null)
            {
                events.StartTime = eventEditVM.StartTime;
            }

            if (eventEditVM.EndTime != null)
            {
                events.EndTime = eventEditVM.EndTime;
            }

            List<EventSpeaker> eventSpeakers = new();
            if (eventEditVM.SpeakerId !=null)
            {
                foreach (var item in eventEditVM.SpeakerId)
                {
                    EventSpeaker eventSpeaker = new();
                    eventSpeaker.SpeakerId = item;
                    eventSpeaker.EventId = events.Id;


                    bool existSpeaker = events.EventSpeakers.Any(es => es.Speakar.Id == item);
                    if (!existSpeaker)
                    {
                        _appDbContext.EventSpeakers.Add(eventSpeaker);
                        eventSpeakers.Add(eventSpeaker);
                    }

                    else
                    {
                        ModelState.AddModelError("SpeakerId", "This speaker exists");
                        return View(eventEditVM);
                    }
                }
            }

            if (eventEditVM.ExistSpeakerId !=null)
            {
                foreach (var item in eventEditVM.ExistSpeakerId)
                {
                    EventSpeaker eventSpeaker=await _appDbContext.EventSpeakers.FirstOrDefaultAsync(es => es.SpeakerId == item);
                    events.EventSpeakers.Remove(eventSpeaker);
                }
            }

            if (eventEditVM.Image != null)
            {
                if (!eventEditVM.Image.IsImage())
                {
                    ModelState.AddModelError("Img", "only img");
                    return View();
                }

                events.ImageUrl = eventEditVM.Image.SaveImage(_env, "img/event", eventEditVM.Image.FileName);
            }

            events.EventSpeakers = eventSpeakers;
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult>Delete(int id)
        {
            if (id == null) return NotFound();
            Event removeEvent = await _appDbContext.Events.FindAsync(id);
            if(removeEvent == null) return NotFound();
            string fullPath = Path.Combine(_env.WebRootPath, "img/event", removeEvent.ImageUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            _appDbContext.Events.Remove(removeEvent);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
