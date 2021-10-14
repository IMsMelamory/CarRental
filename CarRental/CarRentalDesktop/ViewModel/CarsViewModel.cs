using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;

namespace CarRentalDesktop.ViewModel
{
    public class CarsViewModel: CarViewModel
    {
        
        private Car _selectedCar;
        private string _ButtonContent;
        public string ButtonContent
        {
            get => _ButtonContent;
            set
            {
                _ButtonContent = value;
                OnPropertyChanged();
            }
        }
        public CarsViewModel()
        {
            AddNew = new RelayCommand(AddNewCar, IsSelected);
            Remove = new RelayCommand(RemoveCar, IsEnable);
            Clear = new RelayCommand(ClearCar, IsEnable);
            CarRepository= new CarsRepository(new JsonProvider<Car>("cars.json"));
            var list = CarRepository.GetAll();
            Cars = new ObservableCollection<Car>(CarRepository.GetAll());

        }

       public ObservableCollection<Car> Cars { get; set; }
       public CarsRepository CarRepository { get; set; }
       //public List<CarsViewModel> CarsView{ get; set; }
        
        
       
        
        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                Number = value.Number;
                Model = value.Model;
                Color = value.Color;
                DateRelease = value.DateRelease;
                DayPrice = value.DayPrice;
                OnPropertyChanged();
            }
        }
        public RelayCommand AddNew { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Clear { get; set; }
        private void AddNewCar(object arg)
        {
            var car = new Car() { Number = Number, Model = Model, Color = Color, DateRelease = DateRelease, DayPrice = DayPrice, ID = 1};
            Cars.Add(car);
           // SelectedCar = car;
            ClearFields();
            CarRepository.Add(car);
        }

        private void ClearFields()
        {
            Number = string.Empty;
            Color = string.Empty;
            Model = string.Empty;
            DateRelease = string.Empty;
            DayPrice = 0;
        }

        private void RemoveCar(object arg)
        {
            if (SelectedCar != null)
            {
                CarRepository.Remove(SelectedCar);
                Cars.Remove(SelectedCar);
                ClearFields();
            }

            
            
        }
        
        private void ClearCar(object arg)
        {
            if (SelectedCar != null)
            {
                ClearFields();
            }
        }
        private bool IsEnable (object value)
        {
            return SelectedCar != null;
        }
        private bool IsSelected(object value)
        {
            if (SelectedCar != null)
            {
                ButtonContent = "Edit";
            }
            else
            {
                ButtonContent = "Add";
            }
            return true;
            
        }
    }
}
