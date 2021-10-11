using System.Collections.Generic;
using System.Windows.Controls;
using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;

namespace CarRentalDesktop.ViewModel
{
    public class CarsViewModel: CarViewModel
    {
        
        private CarsViewModel _selectedCar;
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
            var carsListViewModel = new CarMapper();
            var carRepository = new CarsRepository(new JsonProvider<Car>("cars.json"));
            //var listCars = carRepository.GetAll();
            //var mylist =carsListViewModel.ListCarsViewModel(carRepository.GetAll()) ; что-то неправильное!!!!!!!
            Cars = new TrulyObservableCollection<CarsViewModel>();
            
        }

       public TrulyObservableCollection<CarsViewModel> Cars { get; set; }
       // public CarsRepository CarRepository { get; set; }
        //public CarMapper CarsListViewModel { get; set; }
        public List<CarsViewModel> CarsView { get; set; } = new List<CarsViewModel>();
        
       
        
        public CarsViewModel SelectedCar
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
            var car = new CarsViewModel() { Number = Number, Model = Model, Color = Color, DateRelease = DateRelease, DayPrice = DayPrice};
            Cars.Add(car);
           // SelectedCar = car;
            ClearFields();
            
            //CarRepository.Add(car);
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
                Cars.Remove(SelectedCar);
                ClearFields();
            }

            //CarRepository.Remove(SelectedCar);
            
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
            if (SelectedCar == null)
            {
                ButtonContent = "Add";
            }
            else
            {
                ButtonContent = "Edit";
            }
            return true;
            
        }
    }
}
