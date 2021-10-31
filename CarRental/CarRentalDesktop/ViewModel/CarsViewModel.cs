using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;
using System.Collections.ObjectModel;

namespace CarRentalDesktop.ViewModel
{
    public class CarsViewModel: BaseViewModel
    {
        
        private CarViewModel _selectedCar;
        private ObservableCollection<CarViewModel> _list = new ObservableCollection<CarViewModel>();
        private string _buttonContent;
        public string ButtonContent
        {
            get => _buttonContent;
            set
            {
                _buttonContent = value;
                OnPropertyChanged();
            }
        }
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
            AddNew = new RelayCommand(AddNewCar, IsSelected);
            Remove = new RelayCommand(RemoveCar, IsEnable);
            Clear = new RelayCommand(ClearCar, IsEnable);
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
        private void AddNewCar(object arg)
        {
            var car = new CarViewModel() { Number = Number, Model = Model, Color = Color, DateRelease = DateRelease, DayPrice = DayPrice};
            Cars.Add(car);
            ClearFields();
            CarRepository.Add(CarVM.ToCar(car));
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
        public override string Header => "Cars";
    }
}
