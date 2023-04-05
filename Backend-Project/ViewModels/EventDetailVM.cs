using Backend_Project.Models;

namespace Backend_Project.ViewModels
{
    public class EventDetailVM
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string EventImageUrl { get; set; }
        public string EventName { get; set; }
        public DateTime EventDateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Venue { get; set; }
        public string EventDesc { get; set; }

        public List<EventSpeaker>? EventSpeakers { get; set; }
        public List<string> CategoryNames { get; set; }
    }
}
