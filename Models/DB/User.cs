using System;
using System.Collections.Generic;

namespace StatiiIncarcare.Models.DB
{
    public partial class User
    {
        public User()
        {
            Rezervares = new HashSet<Rezervare>();
        }

        public int IdUser { get; set; }
        public string? Nume { get; set; }

        public virtual ICollection<Rezervare> Rezervares { get; set; }
    }
}
