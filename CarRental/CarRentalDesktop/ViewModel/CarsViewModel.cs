using System.Collections.ObjectModel;
using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;

namespace CarRentalDesktop.ViewModel
{
    public class CarsViewModel: BaseViewModel
    {
        private string _number;
        private string _model;
        private string _color;
        private string _dateRelease;
        private int _dayPrice;
        private Car _selectedCar;
        
        public CarsViewModel()
        {
            AddNew = new RelayCommand(AddNewCar);
            Remove = new RelayCommand(RemoveCar);
            Clear = new RelayCommand(ClearCar);
            
            CarRepository = new CarsRepository(new JsonProvider<Car>("cars.json"));
            
            Cars = new ObservableCollection<Car>(CarRepository.GetAll());

            var clientsRepository = new ClientsRepository(new JsonProvider<Client>("clients.json"));
            var managersRepository = new ManagersRepository(new JsonProvider<Manager>("managers.json"));
            var rentRepository = new RentsRepository(new JsonProvider<Rent>("rent.json"));

        }

        public CarsRepository CarRepository { get; set; }

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
        public ObservableCollection<Car> Cars { get; set; }
        
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
            var car = new Car() { Number = Number, Model = Model, Color = Color, DateRelease = DateRelease, DayPrice = DayPrice};
            Cars.Add(car);
            SelectedCar = car;
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
    }
}
