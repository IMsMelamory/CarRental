using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalCore.Model;

namespace CarRentalDesktop.ViewModel
{
    public class RentsViewModel: RentViewModel
    {
        private Rent _selectedRent;
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
    }
}
