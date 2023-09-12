using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class DostupneIP:IEntity
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public string Subnet { get; set; }
        public bool Zauzeto { get; set; }
    }
}
