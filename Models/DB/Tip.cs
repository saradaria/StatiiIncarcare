using System;
using System.Collections.Generic;

namespace StatiiIncarcare.Models.DB
{
    public partial class Tip
    {
        public Tip()
        {
            Prizas = new HashSet<Priza>();
        }

        public int IdTip { get; set; }
        public string? Nume { get; set; }

        public virtual ICollection<Priza> Prizas { get; set; }
    }
}
