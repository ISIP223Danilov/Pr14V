using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr14V.Models
{
    public class Session
    {
        public int SessionID { get; set; }
        public int MovieID { get; set; }
        public int HallID { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Hall Hall { get; set; }
    }

}
