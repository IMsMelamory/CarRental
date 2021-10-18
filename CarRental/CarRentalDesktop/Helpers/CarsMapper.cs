using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarRentalCore.Model;
using CarRentalDesktop.Annotations;

namespace CarRentalDesktop.ViewModel
{
    public class CarsMapper : CarViewModel
    {
        public List<CarsViewModel> CarsToVm(List<Car> cars)
        {
            var mylist = cars.Select(x => new CarsViewModel()
            {
                Number = x.Number,
                Model = x.Model,
                Color = x.Color,
                DateRelease = x.DateRelease,
                DayPrice = x.DayPrice,
                ID = x.ID
            }).ToList();
          return mylist;
        }
    }
}
