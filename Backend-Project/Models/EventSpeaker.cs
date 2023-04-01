namespace Backend_Project.Models
{
    public class EventSpeaker
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int SpeakerId { get; set; }
        public Event Event { get; set; }
        public Speaker Speakar { get; set; }
    }
}
