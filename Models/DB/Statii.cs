using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StatiiIncarcare.Models.DB
{
    public partial class Statii
    {
        public Statii()
        {
            Prizas = new HashSet<Priza>();
        }

        public int IdStatie { get; set; }
        //[Required]
        public string? Nume { get; set; }
      //  [Required]
        public string? Oras { get; set; }
        //  [Required]

        public string? Adresa { get; set; }
       

        public virtual ICollection<Priza> Prizas { get; set; }

        internal Task FirstOrDefaultAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
