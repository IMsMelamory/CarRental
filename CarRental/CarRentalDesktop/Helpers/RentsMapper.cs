using CarRentalCore.Model;
using CarRentalDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalDesktop.ViewModel
{
    public class RentsMapper : RentViewModel
    {
        public List<RentViewModel> ToViewModel(List<Rent> managers)
        {
            return managers.Select(x => new RentViewModel()
            {
                CarNumber = x.CarNumber,
                ClientNumberLicense = x.ClientNumberLicense,
                StartRent = x.StartRent.Date,
                EndRent = x.EndRent.Date,
                DayRentCount = x.DayRentCount,
                Fine = x.Fine,
            }).ToList();
        }
        public Rent ToRent(RentViewModel vmRent)
        {
            return new Rent()
            {
                CarNumber = vmRent.CarNumber,
                ClientNumberLicense = vmRent.ClientNumberLicense,
                StartRent = vmRent.StartRent.Date,
                EndRent = vmRent.EndRent.Date,
                DayRentCount = vmRent.DayRentCount,
                Fine = vmRent.Fine,
            };
        }
    }   
}
