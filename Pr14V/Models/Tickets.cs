using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr14V.Models
{
    public class Tickets
    {
        public int TicketID { get; set; }
        public int SessionID { get; set; }
        public int SeatID { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public int UserID { get; set; } // Или то имя, которое у тебя в таблице БД


        public virtual Session Session { get; set; }
        public virtual Seat Seat { get; set; }
    }

}
