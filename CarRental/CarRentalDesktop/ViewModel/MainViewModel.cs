using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDesktop.ViewModel
{
    class MainViewModel: BaseViewModel
    {
        ObservableCollection<object> _children;


        public MainViewModel()
        {
            _children = new ObservableCollection<object>();
            _children.Add(new CarsViewModel());
            _children.Add(new ClientsViewModel());
            _children.Add(new ManagersViewModel());
        }

        public ObservableCollection<object> Children => _children;
    }
}
