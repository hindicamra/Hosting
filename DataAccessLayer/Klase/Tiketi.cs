using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class Tiketi:IEntity
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public DateTime Datum { get; set; }
        public bool Zavrsen { get; set; }

        public Klijenti Klijenti { get; set; }
        public int KlijentiId { get; set; }

        public Admin Admin { get; set; }
        public int? AdminId { get; set; }
        public bool Odgovoreno { get; set; }
    }
}
