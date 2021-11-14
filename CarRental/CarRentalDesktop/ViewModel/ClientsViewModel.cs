using System;
using System.Collections.ObjectModel;
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
        public ClientViewModel CurrentClient { get; set; }
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
            Clients = new ObservableCollection<ClientViewModel>(ClientMap.ToViewModel(ClientsRepository.GetAll()));
        }
        private void AddNew(object arg)
        {
            var client = new ClientViewModel();
            SelectedClient = client;
            client.Name = SelectedClient.Name;
            client.LastName = SelectedClient.LastName;

            ClientsRepository.Add(ClientMap.ToClient(client));
            ClearFields();
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
            if (SelectedClient != null)
            {
                var myClient = ClientsRepository.FindByID(SelectedClient.ID);
                myClient.Name = CurrentClient.Name;
                myClient.LastName = CurrentClient.LastName;
                myClient.SecondLastName = CurrentClient.SecondLastName;
                myClient.NumberDriversLicence = CurrentClient.NumberDriversLicence;
                myClient.ManagerID = CurrentClient.ManagerID;
                ClearFields();
                UpdateClients();
            }
        }

        private void ClearClient(object arg)
         {
             if (SelectedClient != null)
             {
                 ClearFields();
             }
         }
        public override string Header => "Clients";
    }
}
