namespace Backend_Project.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}
