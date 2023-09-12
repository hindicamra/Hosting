using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class HostingPaketi :IEntity
    {
        public int Id { get; set; }
        public string NazivPaketa { get; set; }
        public string Prostor { get; set; }
        public string Promet { get; set; }
        public string Baza { get; set; }
        public string EmailAdresa { get; set; }
        public string Cijena { get; set; }
        public string IPv4 { get; set; }
        public string IPv6 { get; set; }
        public int AddonDomene { get; set; }
        public int ParkedDomene { get; set; }
        public string FTPNaloga { get; set; }
        public string PodDomena { get; set; }

        public Serveri Serveri { get; set; }
        public int ServeriId { get; set; }

        public bool Aktivan { get; set; }
    }
}
