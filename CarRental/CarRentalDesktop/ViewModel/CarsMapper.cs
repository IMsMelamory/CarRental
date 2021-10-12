using System;
using System.Collections.Generic;
using System.Linq;
using CarRentalCore.Model;

namespace CarRentalDesktop.ViewModel
{
    public class CarsMapper: CarViewModel
    {
        public List<CarsViewModel> ListViewModel(List<Car> cars)
        {
            return cars.Select(x => new CarsViewModel()
            {
                Number = x.Number,
                Model = x.Model,
                Color = x.Color,
                DateRelease = x.DateRelease, 
                DayPrice = x.DayPrice, 
                ID = x.ID
            }).ToList();
           
        }
    }
}
