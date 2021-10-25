using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDesktop.ViewModel
{
   public class UserViewModel: BaseViewModel
    {
        private string _name;
        private string _lastName;
        private string _secondLastName;
        private DateTime _bDay;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        public string SecondLastName
        {
            get => _secondLastName;
            set
            {
                _secondLastName = value;
                OnPropertyChanged();
            }
        }
        public DateTime BDay
        {
            get => _bDay;
            set
            {
                _bDay = value;
                OnPropertyChanged();
            }
        }
    }
}
