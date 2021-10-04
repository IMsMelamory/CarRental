using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
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
        private string _dayPrice;
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
        public string dayPrice
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
                number = value.Number;
                model = value.Model;
                color = value.Color;
                dateRelease = value.DateRelease;
                dayPrice = value.DayPrice;
                OnPropertyChanged("SelectedCar");
            }
        }
        public RelayCommand AddNew { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Clear { get; set; }
        public CarsViewModel()
        {
            Cars = new ObservableCollection<Car>();
            AddNew = new RelayCommand(AddNewCar);
            Remove = new RelayCommand(RemoveCar);
            Clear = new RelayCommand(ClearCar);

        }
        private void AddNewCar(object arg)
        {
            var car = new Car() { Number = number, Model = model, Color = color, DateRelease = dateRelease, DayPrice = dayPrice};
            Cars.Add(car);
            //SelectedCar = car;
            number = string.Empty;
            color = string.Empty;
            model = string.Empty;
            dateRelease = string.Empty;
            dayPrice = string.Empty;
        }
        private void RemoveCar(object arg)
        {
            if (SelectedCar != null)
            {
                Cars.Remove(SelectedCar);
                number = string.Empty;
                color = string.Empty;
                model = string.Empty;
                dateRelease = string.Empty;
                dayPrice = string.Empty;
            }


        }
        private void ClearCar(object arg)
        {
            if (SelectedCar != null)
            {
                number = string.Empty;
                color = string.Empty;
                model = string.Empty;
                dateRelease = string.Empty;
                dayPrice = string.Empty;
            }
        }


    }
}
