namespace WpfUI.Models
{
    public class CalculatedEvent
    {
        public Event Event { get; }

        public int RemainingDays { get; }

        public CalculatedEvent(Event @event, int remainingDays)
        {
            Event = @event;
            RemainingDays = remainingDays;
        }
    }
}
