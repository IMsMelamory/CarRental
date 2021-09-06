using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
   public  class Rent
    {
        public int ClientID { get; set; }
        public int CarId { get; set; }
        public DateTime StartRent { get; set; }
        public DateTime EndRent { get; set; }
        public int Fine { get; set; }
    }
}
