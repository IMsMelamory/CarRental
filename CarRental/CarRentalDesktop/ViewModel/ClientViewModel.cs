using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDesktop.ViewModel
{
    public class ClientViewModel: UserViewModel
    {
        private string _numberDriversLicence;
        private int _managerID;
        public string NumberDriversLicence
        {
            get => _numberDriversLicence;
            set
            {
                _numberDriversLicence = value;
                OnPropertyChanged();
            }
        }
        public int ManagerID
        {
            get => _managerID;
            set
            {
                _managerID = value;
                OnPropertyChanged();
            }
        }
    }
}
