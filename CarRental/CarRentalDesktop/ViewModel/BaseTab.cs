using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDesktop.ViewModel
{
    public class BaseTab : BaseViewModel
    {
        public ObservableCollection<object> Tab { get; set; }
    }
            
}
