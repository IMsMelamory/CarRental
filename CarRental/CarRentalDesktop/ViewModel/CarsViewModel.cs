using System.Collections.ObjectModel;
using System.Configuration;
using System.Runtime.Serialization.Formatters;
using CarRentalDesktop.Model;

namespace CarRentalDesktop.ViewModel
{
    public class CarsViewModel: BaseViewModel
    {
        private string _number;
        private string _model;
        private string _color;
        private string _dateRelease;
        private int _dayPrice;
        public string number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }
        public string model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged("Model");
            }
        }
        public string color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }
        public string dateRelease
        {
            get => _dateRelease;
            set
            {
                _dateRelease = value;
                OnPropertyChanged("DateRelease");
            }
        }
        public int dayPrice
        {
            get => _dayPrice;
            set
            {
                _dayPrice = value;
                OnPropertyChanged("DayPrice");
            }
        }
        private Car _selectedCar;
        public ObservableCollection<Car> Cars { get; set; }
        public Car SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                _selectedCar = value;
                OnPropertyChanged("SelectedCar");
            }
        }
        public RelayCommand AddNew { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Edit { get; set; }
        public CarsViewModel()
        {
            Cars = new ObservableCollection<Car>();
            AddNew = new RelayCommand(AddNewCar);
            Remove = new RelayCommand(RemoveCar);

        }
        private void AddNewCar(object arg)
        {
            var car = new Car() { Number = number, Model = model, Color = color, DateRelease = dateRelease, DayPrice = dayPrice};
            Cars.Add(car);
            number = "";
            color = "";
            model = "";
        }
        private void RemoveCar(object arg)
        {
            Cars.Remove(SelectedCar);
        }
        private void EditCar(object arg)
        {
            if (SelectedCar != null)
            {

            }
        }


    }
}
