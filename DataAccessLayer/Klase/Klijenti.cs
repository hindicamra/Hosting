using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class Klijenti:IEntity
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Firma { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Mobitel { get; set; }
        public string Fax { get; set; }
        public string JMBG { get; set; }
        public string IDBroj { get; set; }
        public string PDVBroj { get; set; }
        public string Password { get; set; }

        public Grad Grad { get; set; }
        public int GradId { get; set; }
    }
}
