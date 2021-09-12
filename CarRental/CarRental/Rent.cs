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
        public string ClientNumberLicense { get; set; }
        public string CarNumber { get; set; }
        public DateTime StartRent { get; set; }
        public DateTime EndRent { get; set; }
        public int DayRentCount { get; set; }
        public int Fine { get; set; }
    }
}
