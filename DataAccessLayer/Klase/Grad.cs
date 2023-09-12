using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class Grad:IEntity
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public Kanton Kanton { get; set; }
        public int KantonId { get; set; }
    }
}
