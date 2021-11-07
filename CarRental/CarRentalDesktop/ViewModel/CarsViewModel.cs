using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;
using System.Collections.ObjectModel;
using System.Windows;

namespace CarRentalDesktop.ViewModel
{
    public class CarsViewModel: BaseViewModel
    {
        
        private CarViewModel _selectedCar;
        private ObservableCollection<CarViewModel> _list = new ObservableCollection<CarViewModel>();
        private string _number;
        private string _model;
        private string _color;
        private string _dateRelease;
        private int _dayPrice;
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
        public CarsViewModel()
        {
            AddNew = new RelayCommand(AddNewCar);
            Remove = new RelayCommand(RemoveCar, RemoveCar => SelectedCar != null);
            Clear = new RelayCommand(ClearCar);
            Save = new RelayCommand(SaveCar, SaveCar => SelectedCar != null);
            CarRepository = new CarsRepository(new JsonProvider<Car>("cars.json"));
            Cars = new ObservableCollection<CarViewModel>(CarVM.ToViewModel(CarRepository.GetAll()));
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
        public RelayCommand Save { get; set; }
        private void AddNewCar(object arg)
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

        private void RemoveCar(object arg)
        {
            if (SelectedCar != null)
            {
                CarRepository.Remove(CarVM.ToCar(SelectedCar));
                Cars.Remove(SelectedCar);
                ClearFields();
            }   
        }
        private void SaveCar(object arg)
        {
            if (SelectedCar != null)
            {
                var myCar = CarRepository.FindByID(SelectedCar.ID);
                myCar.Number = Number;
                myCar.Model = Model;
                myCar.Color = Color;
                myCar.DayPrice = DayPrice;
                myCar.DateRelease = DateRelease;
                CarRepository.ForceUpdate();
                SelectedCar.Number = Number;
                SelectedCar.Model = Model;
                SelectedCar.Color = Color;
                SelectedCar.DayPrice = DayPrice;
                SelectedCar.DateRelease = DateRelease;
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
        public override string Header => "Cars";
    }
}
