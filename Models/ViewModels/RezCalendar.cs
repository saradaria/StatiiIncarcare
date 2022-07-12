namespace StatiiIncarcare.Models.ViewModels
{
    public class RezCalendar
    {
        public DateTime DataIn { get; set; }
        public DateTime DataOut { get; set; }
        public string NrMasina { get; set; }
        public DayOfWeek ZiCurenta { get; set; } 

    }
}
