namespace StatiiIncarcare.Models.ViewModels
{
    public class WeekCalendar
    {
        public List<RezCalendar> Week { get; set; }

        public DateTime CurentMonday { get; set; }

        public int IdPriza { get; set; }
    }
}
