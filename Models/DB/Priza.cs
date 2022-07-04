using System;
using System.Collections.Generic;

namespace StatiiIncarcare.Models.DB
{
    public partial class Priza
    {
        public Priza()
        {
            Rezervares = new HashSet<Rezervare>();
        }

        public int IdPriza { get; set; }
        public int? IdTip { get; set; }
        public int? IdStatie { get; set; }

        public virtual Statii? IdStatieNavigation { get; set; }
        public virtual Tip? IdTipNavigation { get; set; }
        public virtual ICollection<Rezervare> Rezervares { get; set; }
    }
}
