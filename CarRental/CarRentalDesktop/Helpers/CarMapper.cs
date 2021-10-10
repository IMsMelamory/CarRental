using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Documents;
using CarRentalCore.Model;
using CarRentalDesktop.ViewModel;

namespace CarRentalDesktop.Helpers
{
   public class CarMapper 
    {
        public List<CarsViewModel> ListCarsViewModel(List<Car> cars)
        {
            return cars.Select(x => new CarsViewModel(){Number = x.Number}).ToList();
        }
        public List<Car> ListCars (List<CarsViewModel> carsViewModel)
        {
            return carsViewModel.Select(x => new Car() { Number = x.Number }).ToList();
        }
    }
}
