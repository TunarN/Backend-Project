using Backend_Project.DAL;
using Backend_Project.Models;
using Backend_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Project.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public EventController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            List<Event> events = _appDbContext.Events.ToList();
            return View(events);
        }
        public IActionResult Detail(int id)
        {
            if (id == null) return NotFound();
            Event even = _appDbContext.Events.Include(e=>e.EventSpeakers).ThenInclude(m=>m.Speakar).FirstOrDefault(e=>e.Id==id);
            var category = _appDbContext.Categories.ToList();
            EventDetailVM eventDetailVM = new();
            eventDetailVM.EventImageUrl = even.ImageUrl;
            eventDetailVM.EventName= even.EventName;
            eventDetailVM.EventDesc = even.EventDesc;
            eventDetailVM.EventDateTime = even.EventDateTime;
            eventDetailVM.StartTime= even.StartTime;
            eventDetailVM.EndTime= even.EndTime;
            eventDetailVM.Venue= even.Venue;
            eventDetailVM.even = even;

            List<string> categoryNames = new();
            foreach (var item in category)
            {
                categoryNames.Add(item.CategoryName);
            }
            eventDetailVM.CategoryNames = categoryNames;

            return View(eventDetailVM);
        }
    }
}
