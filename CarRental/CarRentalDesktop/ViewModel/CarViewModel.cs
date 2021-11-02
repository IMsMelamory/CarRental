

namespace CarRentalDesktop.ViewModel
{
    public class CarViewModel:BaseViewModel
    {
        private string _number;
        private string _model;
        private string _color;
        private string _dateRelease;
        private int _dayPrice;
        private int _id;
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
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
    }
}
