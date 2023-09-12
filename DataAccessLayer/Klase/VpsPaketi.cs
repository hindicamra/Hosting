using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class VpsPaketi : IEntity
    {
        public int Id { get; set; }
        public string NazivPaketa { get; set; }
        public string HDD { get; set; }
        public string Promet { get; set; }
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string SWAP { get; set; }
        public string OS { get; set; }
        public string IPv4 { get; set; }
        public string IPv6 { get; set; }
        public string Konfiguracija { get; set; }
        public string Cijena { get; set; }

        public int ServeriId { get; set; }
        public Serveri Serveri { get; set; }
        public bool Deaktiviran { get; set; }
    }
}
