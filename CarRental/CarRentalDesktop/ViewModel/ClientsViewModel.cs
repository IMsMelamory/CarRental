using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;

namespace CarRentalDesktop.ViewModel
{
    public class ClientsViewModel :BaseTab

    {
        private ClientViewModel _selectedClient;
        private ClientViewModel _currentClient;
        private ObservableCollection<ClientViewModel> _clients;
        public ClientsViewModel(ManagersViewModel managerVM)
        {
            ManagerVM = managerVM;
            AddNewClient = new RelayCommand(AddNew);
            RemoveClient = new RelayCommand(Remove, RemoveClient => SelectedClient != null);
            SaveClient = new RelayCommand(Save, SaveClient => SelectedClient != null);
            Clear = new RelayCommand(ClearClient);
            ClientsRepository = new ClientsRepository(new JsonProvider<Client>("clients.json"));
            UpdateClients();
        }
        public ManagersViewModel ManagerVM { get; set; }
        public ClientViewModel CurrentClient
        {
            get => _currentClient;

            set
            {
                _currentClient = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ClientViewModel> Clients
        {
            get => _clients;

            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }
        public ClientsRepository ClientsRepository { get; set; }
        public ClientsMapper ClientMap { get; set; } = new ClientsMapper();
        public ClientViewModel SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                CurrentClient = SelectedClient;
                OnPropertyChanged();
            }
        }
        public RelayCommand AddNewClient { get; set; }
        public RelayCommand RemoveClient { get; set; }
        public RelayCommand Clear { get; set; }
        public RelayCommand SaveClient { get; set; }
        private void UpdateClients()
        {
            Clients = new ObservableCollection<ClientViewModel>(ClientMap.ToViewModel(ClientsRepository.GetAll()).OrderBy(x => x.ID));
        }
        private void AddNew(object arg)
        {
            CurrentClient = new ClientViewModel();
            //SelectedClient = new ClientViewModel();
            /*var client = new ClientViewModel()
            {
                Name = CurrentClient.Name,
                LastName = CurrentClient.LastName,
                SecondLastName = CurrentClient.SecondLastName,
                BDay = CurrentClient.BDay,
                ID = 0
            };*/
            ClientsRepository.Add(ClientMap.ToClient(CurrentClient));
            //ClearFields();
            UpdateClients();
        }

        private void ClearFields()
        {
            SelectedClient = null;
        }

        private void Remove(object arg)
        {
            
            ClientsRepository.RemoveById(ClientMap.ToClient(SelectedClient).ID);
            UpdateClients();
            ClearFields();
                

        }
        private void Save(object arg)
        {
            if (SelectedClient == null)
            {
                return;
            }
            ClientsRepository.EditById(SelectedClient.ID, ClientMap.ToClient(CurrentClient));
            ClearFields();
            UpdateClients();
        }

        private void ClearClient(object arg)
         {
                 ClearFields();
         }
        public override string Header => "Clients";
    }
}
