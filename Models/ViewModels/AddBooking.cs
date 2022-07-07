using StatiiIncarcare.Models.DB;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StatiiIncarcare.Models.ViewModels

{
    public class AddBooking
    {
        public int IdPriza { get; set; }
        public int IdStatie { get; set; }

        [Required(ErrorMessage = "Adauga data in pentru rezervare")]
        public DateTime? TimeIn { get; set; }
        [Required(ErrorMessage = "Adauga data out pentru rezervare")]
        public DateTime? TimeOut { get; set; }

        [Required(ErrorMessage = "Adauga numarul masinii dorite")]
        public string? NrMasina { get; set; }
    }
}
