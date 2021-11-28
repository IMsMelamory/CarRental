using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDesktop.ViewModel
{
    public class ManagerViewModel: UserViewModel
    {
        private DateTime _startWork;
        private DateTime _endWork;
        public DateTime StartWork
        {
            get => _startWork;
            set
            {
                _startWork = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndWork
        {
            get => _endWork;
            set
            {
                _endWork = value;
                OnPropertyChanged();
            }
        }

    }
}
