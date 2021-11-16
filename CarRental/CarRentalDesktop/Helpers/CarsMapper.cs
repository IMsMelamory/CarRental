using System;
using System.Collections.Generic;
using System.Linq;
using CarRentalCore.Model;

namespace CarRentalDesktop.ViewModel
{
    public class CarsMapper : CarViewModel
    {
        public List<CarViewModel> ToViewModel(List<Car> cars)
        {
          return cars.Select(x => new CarViewModel()
          {
              Number = x.Number,
              Model = x.Model,
              Color = x.Color,
              DateRelease = x.DateRelease,
              DayPrice = x.DayPrice,
              ID = x.ID
          }).ToList();
        }
        public Car ToCar(CarViewModel vmCar)
        {
            if (string.IsNullOrEmpty(vmCar.Color))
            {
                throw new Exception("Длинная длинна слишком коротка");
            }
            
            return new Car()
            {
                Number = vmCar.Number,
                Model = vmCar.Model,
                Color = vmCar.Color,
                DateRelease = vmCar.DateRelease,
                DayPrice = vmCar.DayPrice,
                ID = vmCar.ID
            };
        }
    }
}
