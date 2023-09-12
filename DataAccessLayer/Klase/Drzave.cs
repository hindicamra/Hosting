using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class Drzave:IEntity
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
}
