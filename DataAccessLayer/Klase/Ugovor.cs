using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class Ugovor:IEntity
    {
        public int Id { get; set; }
        public DateTime DatumKreiranjaZahtjeva { get; set; }
        public string IP { get; set; }
        public bool Hidden { get; set; }
        public DateTime DatumOdobrenja { get; set; }
        public DateTime DatumVazenja { get; set; }
        public string SSLZaDomenu { get; set; }

        public virtual HostingPaketi HostingPaketi { get; set; }
        public int? HostingPaketiId { get; set; }

        public virtual DomenePaketi Domena { get; set; }
        public int? DomenaId { get; set; }
        public string ZakupljeniDomen { get; set; }

        public virtual DedicatedPaketi DedicatedPaketi { get; set; }
        public int? DedicatedPaketiId { get; set; }

        public virtual VpsPaketi VpsPaketi { get; set; }
        public int? VpsPaketiId { get; set; }

        public virtual Klijenti Klijenti { get; set; }
        public int? KlijentiId { get; set; }

        public virtual SSLPaketi SSLPaketi { get; set; }
        public int? SSLPaketiId { get; set; }

        public virtual Status Status { get; set; }
        public int? StatusId { get; set; }

        public string boja { get; set; }
        public bool produzenje { get; set; }

        public string PristupnaAdresa { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
