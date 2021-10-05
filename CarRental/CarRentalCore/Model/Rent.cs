using System;

namespace CarRentalCore.Model
{
   public  class Rent : BaseEntity
    {
        public string ClientNumberLicense { get; set; }
        public string CarNumber { get; set; }
        public DateTime StartRent { get; set; }
        public DateTime EndRent { get; set; }
        public int DayRentCount { get; set; }
        public double Fine { get; set; }
    }
}
