namespace Backend_Project.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string EventName { get; set; }
        public DateTime EventDateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Venue { get; set; }
        public string EventDesc { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}
