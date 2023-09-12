using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class TiketiEditViewModel
    {
        [Required(ErrorMessage ="*Naslov je obavezno polje.")]
        public string Naslov { get; set; }

        [Required(ErrorMessage = "*Poruka je obavezno polje.")]
        public string Poruka { get; set; }
    }
}
