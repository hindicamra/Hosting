using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class KlijentLog:IEntity
    {
        public int Id { get; set; }
        public Klijenti Klijent { get; set; }
        public int? KlijentId { get; set; }
        public DateTime Datum { get; set; }
        public string IP { get; set; }
        public bool Hidden { get; set; }
        public string Browser { get; set; }
        public string Hostname { get; set; }
        public string Poruka { get; set; }
    }
}
