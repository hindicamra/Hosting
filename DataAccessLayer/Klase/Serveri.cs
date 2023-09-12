using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class Serveri:IEntity
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string HDD { get; set; }
        public bool OnOf { get; set; }

        public Rack Rack { get; set; }
        public int RackId { get; set; }

        public DostupneIP DostupneIP { get; set; }
        public int DostupneIPId { get; set; }
    }
}
