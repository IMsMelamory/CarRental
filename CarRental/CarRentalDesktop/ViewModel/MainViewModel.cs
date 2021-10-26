using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDesktop.ViewModel
{
    class MainViewModel: BaseTab
    {
       
        public MainViewModel()
        {
            Tab = new ObservableCollection<object>();
            Tab.Add(new CarsViewModel());
            Tab.Add(new ClientsViewModel());
            Tab.Add(new ManagersViewModel());
            SelectedTab = (BaseTab)Tab.FirstOrDefault();
        }
    }
}
