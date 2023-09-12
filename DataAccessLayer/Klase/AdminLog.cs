using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class AdminLog:IEntity
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string IP { get; set; }
        public bool Hidden { get; set; }
        public Admin Admin { get; set; }
        public int? AdminId { get; set; }
        public string Poruka { get; set; }
    }
}
