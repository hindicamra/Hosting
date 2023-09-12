using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class Domena:IEntity
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public bool Deaktiviran { get; set; }
    }
}
