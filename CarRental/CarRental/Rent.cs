using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities;
using CarRental.Repositories;

namespace CarRental
{
   public  class Rent : BaseEntity
    {
        public int ClientID { get; set; }
        public int CarId { get; set; }
        public DateTime StartRent { get; set; }
        public DateTime EndRent { get; set; }
        public int Fine { get; set; }
    }
}
