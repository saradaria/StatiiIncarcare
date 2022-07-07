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
       //   [Required(ErrorMessage = "Adauga numele statiei")]
        public string? Nume { get; set; }
       //   [Required(ErrorMessage = "Adauga orasul statiei")]
        public string? Oras { get; set; }

         // [Required(ErrorMessage = "Adauga adresa statiei")]
        public string? Adresa { get; set; }
       

        public virtual ICollection<Priza> Prizas { get; set; }

        internal Task FirstOrDefaultAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
