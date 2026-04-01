using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr14V.Models
{
    public class Hall
    {
        public int HallID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }

}
