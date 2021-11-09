using System.Collections.ObjectModel;

namespace CarRentalDesktop.ViewModel
{
    public class BaseTab : BaseViewModel
    {
        public ObservableCollection<BaseTab> Tab { get; set; }
    }
            
}
