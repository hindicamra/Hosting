using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class Produzenja:IEntity
    {
        public int Id { get; set; }
        public DateTime DatumZahtjeva { get; set; }
        public DateTime DatumRealizacije { get; set; }

        public Ugovor Ugovor { get; set; }
        public int UgovorId { get; set; }
    }
}
