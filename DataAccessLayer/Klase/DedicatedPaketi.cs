using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class DedicatedPaketi : IEntity
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string CPU { get; set; }
        public string Ram { get; set; }
        public string HDDPrimar { get; set; }
        public string HDDSekundar { get; set; }
        public string OS { get; set; }
        public string Promet { get; set; }
        public string IPv4 { get; set; }
        public string IPv6 { get; set; }
        public bool Nadogradnja { get; set; }
        public double Cijena { get; set; }

        public Serveri Serveri { get; set; }
        public int ServeriId { get; set; }
        public bool Deaktivirano { get; set; }
    }
}
