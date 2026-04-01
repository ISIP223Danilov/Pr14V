using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr14V.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public int SessionID { get; set; }
        public int SeatID { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }

        
        public virtual Session Session { get; set; }
        public virtual Seat Seat { get; set; }
    }

}
