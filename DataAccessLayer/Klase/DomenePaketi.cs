using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class DomenePaketi : IEntity
    {
        public int Id { get; set; }
        public string CijenaAktivacije { get; set; }
        public string CijenaZaPrvuGodinu { get; set; }
        public string SvakaNarednaGodina { get; set; }
        public string CijenaUzWebHosting { get; set; }

        public Domena Domena { get; set; }
        public int DomenaId { get; set; }
        public bool Deaktiviran { get; set; }
    }
}
