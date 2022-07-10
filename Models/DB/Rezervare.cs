using System;
using System.Collections.Generic;

namespace StatiiIncarcare.Models.DB
{
    public partial class Rezervare
    {
        public Guid IdRezervare { get; set; }
        public int IdPriza { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public string? NrMasina { get; set; }
        public int? IdUser { get; set; }

        public virtual Priza IdPrizaNavigation { get; set; } = null!;
        public virtual User? IdUserNavigation { get; set; }
    }
}
