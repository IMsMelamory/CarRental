
using System.Collections.ObjectModel;


namespace CarRentalDesktop.ViewModel
{
    class MainViewModel: BaseTab
    {
       
        public MainViewModel()
        {
            Tab = new ObservableCollection<object>
            {
                new CarsViewModel(),
                new ClientsViewModel( new ManagersViewModel()),
                new ManagersViewModel(),
                new RentsViewModel(new ClientsViewModel(new ManagersViewModel()), new CarsViewModel())
            };
        }
    }
}
