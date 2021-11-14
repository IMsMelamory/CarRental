using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;
using System.Collections.ObjectModel;
using System.Windows;

namespace CarRentalDesktop.ViewModel
{
    public class CarsViewModel: BaseTab
    {
        
        private CarViewModel _selectedCar;
        private ObservableCollection<CarViewModel> _list = new ObservableCollection<CarViewModel>();
        private string _number;
        private string _model;
        private string _color;
        private string _dateRelease;
        private int _dayPrice;
        public CarsViewModel()
        {
            AddNewCar = new RelayCommand(AddNew, AddNew => SelectedCar == null);
            RemoveCar = new RelayCommand(Remove, RemoveCar => SelectedCar != null);
            Clear = new RelayCommand(ClearCar);
            SaveCar = new RelayCommand(Save, SaveCar => SelectedCar != null);
            CarRepository = new CarsRepository(new JsonProvider<Car>("cars.json"));
            UpdateCars();
        }
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }
        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }
        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }
        public string DateRelease
        {
            get => _dateRelease;
            set
            {
                _dateRelease = value;
                OnPropertyChanged();
            }
        }
        public int DayPrice
        {
            get => _dayPrice;
            set
            {
                _dayPrice = value;
                OnPropertyChanged();
            }
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
                    Number = value.Number;
                    Model = value.Model;
                    Color = value.Color;
                    DateRelease = value.DateRelease;
                    DayPrice = value.DayPrice;
                }
                
                OnPropertyChanged();
            }
        }
        public RelayCommand AddNewCar { get; set; }
        public RelayCommand RemoveCar { get; set; }
        public RelayCommand Clear { get; set; }
        public RelayCommand SaveCar { get; set; }
        private void UpdateCars()
        {
            Cars = new ObservableCollection<CarViewModel>(CarVM.ToViewModel(CarRepository.GetAll()));
        }
       
        private void AddNew(object arg)
        {
            if (DayPrice <= 0)
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
            }
        }

        private void ClearFields()
        {
            Number = string.Empty;
            Color = string.Empty;
            Model = string.Empty;
            DateRelease = string.Empty;
            DayPrice = 0;
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
            if (DayPrice <= 0)
            {
                MessageBox.Show("Цена аренды должна быть >0");
            }
            else
            {
                var myCar = CarRepository.FindByID(SelectedCar.ID);
                myCar.Number = Number;
                myCar.Model = Model;
                myCar.Color = Color;
                myCar.DayPrice = DayPrice;
                myCar.DateRelease = DateRelease;
                CarRepository.ForceUpdate();
                ClearFields();
                UpdateCars();
            }
        }
        private void ClearCar(object arg)
        {
            if (SelectedCar == null)
            {
                return;
            }
            ClearFields();
            SelectedCar = null;
        }
        public override string Header => "Cars";
    }
}
