using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class Admin:IEntity
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public bool AdminHead { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Grad Grad { get; set; }
        public int GradId { get; set; }
        public bool Izbrisan { get; set; }
    }
}
