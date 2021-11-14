using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace CarRentalDesktop.ViewModel
{
    public class CarsViewModel: BaseTab
    {
        
        private CarViewModel _selectedCar;
        private CarViewModel _currentCar;
        private ObservableCollection<CarViewModel> _list = new ObservableCollection<CarViewModel>();
        public CarsViewModel()
        {
            AddNewCar = new RelayCommand(AddNew, AddNew => SelectedCar == null);
            RemoveCar = new RelayCommand(Remove, RemoveCar => SelectedCar != null);
            Clear = new RelayCommand(ClearCar);
            SaveCar = new RelayCommand(Save, SaveCar => SelectedCar != null);
            CarRepository = new CarsRepository(new JsonProvider<Car>("cars.json"));
            UpdateCars();
        }
        public ObservableCollection<CarViewModel> Cars
        {
            get => _list;

            set { _list = value; OnPropertyChanged(); }
        }
        public CarsRepository CarRepository { get; set; }
        public CarsMapper CarVM { get; set; } = new CarsMapper();
        public CarViewModel SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                if (SelectedCar != null)
                {
                    CurrentCar = SelectedCar;
                }
                
                OnPropertyChanged();
            }
        }
        public CarViewModel CurrentCar
        {
            get => _currentCar;
            set
            {
                _currentCar = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand AddNewCar { get; set; }
        public RelayCommand RemoveCar { get; set; }
        public RelayCommand Clear { get; set; }
        public RelayCommand SaveCar { get; set; }
        private void UpdateCars()
        {
            Cars = new ObservableCollection<CarViewModel>(CarVM.ToViewModel(CarRepository.GetAll()).OrderBy(x => x.ID));
        }
       
        private void AddNew(object arg)
        {
            /*if (DayPrice <= 0)
            {
                MessageBox.Show("Цена аренды должна быть >0");
            }
            else
            {

                var car = new CarViewModel() 
                { 
                    Number = Number, 
                    Model = Model, 
                    Color = Color, 
                    DateRelease = DateRelease, 
                    DayPrice = DayPrice, 
                    ID = CarRepository.FindMaxIDCar() + 1 };
                Cars.Add(car);
                ClearFields();
                CarRepository.Add(CarVM.ToCar(car));
            }*/
        }

        private void ClearFields()
        {
            CurrentCar.Number = string.Empty;
            CurrentCar.Color = string.Empty;
            CurrentCar.Model = string.Empty;
            CurrentCar.DateRelease = string.Empty;
            CurrentCar.DayPrice = 0;
        }

        private void Remove(object arg)
        {
            if (SelectedCar == null)
            {
                return;
            }
            CarRepository.RemoveById(CarVM.ToCar(SelectedCar).ID);
            ClearFields();
            UpdateCars();
        }
        private void Save(object arg)
        {
            if (SelectedCar == null)
            {
                return; 
            }
            if (CurrentCar.DayPrice <= 0)
            {
                MessageBox.Show("Цена аренды должна быть >0");
            }
            else
            {
                CarRepository.EditById(SelectedCar.ID, CarVM.ToCar(CurrentCar));
                ClearFields();
                UpdateCars();
            }
        }
        private void ClearCar(object arg)
        {
            ClearFields();
            SelectedCar = null;
        }
        public override string Header => "Cars";
    }
}
