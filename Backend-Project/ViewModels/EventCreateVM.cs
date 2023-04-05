using Backend_Project.Models;

namespace Backend_Project.ViewModels
{
    public class EventCreateVM
    {
        public string Name { get; set; }
        public DateTime EventDateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Venue { get; set; }
        public string EventDesc { get; set; }
        public List<int>? SpeakerId { get; set; }
        public List<int>? ExistSpeakerId { get; set; }
        public List<EventSpeaker>? EventSpeakers { get; set; }
        public IFormFile Image { get; set; }
    }
}
