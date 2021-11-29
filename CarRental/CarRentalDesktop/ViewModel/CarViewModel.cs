

using CarRentalDesktop.Annotations;
using System;
using System.ComponentModel;

namespace CarRentalDesktop.ViewModel
{
    public class CarViewModel:BaseViewModel, IDataErrorInfo
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
        public string this[string propertyName]
        {
            get
            {
                string result = null;
                switch (propertyName)
                {
                    case "Number":
                    if (string.IsNullOrEmpty(this.Number))
                    {
                        result = "Не может быть пустым!";
                    }
                        break;
                    case "Model":
                        if (string.IsNullOrEmpty(this.Model))
                        {
                            result = "Не может быть пустым!";
                        }
                        break;
                    case "Color":
                        if (string.IsNullOrEmpty(this.Color))
                        {
                            result = "Не может быть пустым!";
                        }
                        break;
                    case "DateRelease":
                        if (string.IsNullOrEmpty(this.DateRelease))
                        {
                            result = "Не может быть пустым!";
                        }
                        break;
                    case "DayPrice":
                        if (DayPrice <= 0)
                        {
                            result = "Цена за сутки не может быть 0!";
                        }
                        break;

                }
                return result;
            }
        }

        // Not using this
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
