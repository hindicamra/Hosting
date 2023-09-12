using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class SSLPaketi : IEntity
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string VrstaProvjere { get; set; }
        public string NivoPovjerenja { get; set; }
        public bool ZelenaTraka { get; set; }
        public string GrancijaIzdavaca { get; set; }
        public string BrojOsiguranihPoddomena { get; set; }
        public string BrzinaIzdavanja { get; set; }
        public string SSLEnkripcija { get; set; }
        public bool Kompaktibilno { get; set; }
        public bool KompaktibilnoSaMobitelima { get; set; }
        public string Cijena { get; set; }
        public bool Deaktivirano { get; set; }
    }
}
