using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Klase
{
    public class Poruke:IEntity
    {
        public int Id { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime Datum { get; set; }

        public Admin Admin { get; set; }
        public int? AdminId { get; set; }

        public Tiketi Tiketi { get; set; }
        public int TiketiId { get; set; }
    }
}
