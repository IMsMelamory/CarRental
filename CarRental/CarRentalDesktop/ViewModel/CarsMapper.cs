using System;
using System.Collections.Generic;
using System.Linq;
using CarRentalCore.Model;

namespace CarRentalDesktop.ViewModel
{
    public class CarsMapper: BaseViewModel
    {
        public List<CarsViewModel> ListViewModel(List<Car> cars)
        {
            return cars.ConvertAll(x => new CarsViewModel());
        }
        public List<Car> ListCars(List<CarsViewModel> carsViewModel)
        {
            return carsViewModel.ConvertAll(x => new Car());
        }
    }
}
