using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr14V.Models
{
    public class Seat
    {
        public int SeatID { get; set; }
        public int HallID { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }

        public virtual Hall Hall { get; set; }
    }

}
