
using System.Collections.ObjectModel;


namespace CarRentalDesktop.ViewModel
{
    class MainViewModel: BaseTab
    {
        public MainViewModel()
        {
        var carsViewModel = new CarsViewModel();
        var managersViewModel = new ManagersViewModel();
        var clientsViewModel = new ClientsViewModel(managersViewModel);
        var rentsViewModel = new RentsViewModel(clientsViewModel, carsViewModel);
        Tab = new ObservableCollection<BaseTab>
        {
        carsViewModel,
        clientsViewModel,
        managersViewModel,
        rentsViewModel
                
        };
        }
    }
}
