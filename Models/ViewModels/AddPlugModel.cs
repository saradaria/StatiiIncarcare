using StatiiIncarcare.Models.DB;
using System.ComponentModel;

namespace StatiiIncarcare.Models.ViewModels
  
{
    public class AddPlugModel
    {
        public int IdStatie { get; set; }
        [DisplayName ("Tipuri de plug")]
        public IList<Tip>? TipPrize { get; set; }

        public int IdTip { get; set; }
    }
}
