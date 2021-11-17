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
        public override string Header => "Clients";
        private ClientViewModel _selectedClient;
        private ClientViewModel _currentClient = new ClientViewModel();
        private ObservableCollection<ClientViewModel> _clients;
        public ClientsViewModel(ManagersViewModel managerVM)
        {
            ManagerVM = managerVM;
            AddNewClientCommand = new RelayCommand(AddNewExecute);
            RemoveClientCommand = new RelayCommand(RemoveExecute, RemoveClient => SelectedClient != null);
            SaveClientCommand = new RelayCommand(SaveExecute, SaveClient => SelectedClient != null);
            ClearCommand = new RelayCommand(ClearClientExecute);
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
        public RelayCommand AddNewClientCommand { get; set; }
        public RelayCommand RemoveClientCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand SaveClientCommand { get; set; }
        private void UpdateClients()
        {
            Clients = new ObservableCollection<ClientViewModel>(ClientMap.ToViewModel(ClientsRepository.GetAll()).OrderBy(x => x.ID));
        }
        private void AddNewExecute(object arg)
        {
            CurrentClient.ID = ClientsRepository.FindMaxIDClient() + 1;
            ClientsRepository.Add(ClientMap.ToClient(CurrentClient));
            ClearFields();
            UpdateClients();
        }

        private void ClearFields()
        {
            SelectedClient = new ClientViewModel();
        }

        private void RemoveExecute(object arg)
        {
            
            ClientsRepository.RemoveById(ClientMap.ToClient(SelectedClient).ID);
            UpdateClients();
            ClearFields();
                

        }
        private void SaveExecute(object arg)
        {
            if (SelectedClient == null)
            {
                return;
            }
            ClientsRepository.EditById(SelectedClient.ID, ClientMap.ToClient(CurrentClient));
            ClearFields();
            UpdateClients();
        }

        private void ClearClientExecute(object arg)
         {
            ClearFields();
         }
       
    }
}
