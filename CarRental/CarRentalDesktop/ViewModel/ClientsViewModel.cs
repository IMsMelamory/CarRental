using System;
using System.Collections.ObjectModel;
using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;

namespace CarRentalDesktop.ViewModel
{
    class ClientsViewModel: ClientViewModel
    {
        private Client _selectedClient;
        private string _buttonContentClient;
        public string ButtonContentClient
        {
            get => _buttonContentClient;
            set
            {
                _buttonContentClient = value;
                OnPropertyChanged();
            }
        }
        public ClientsViewModel()
        {
            AddNew = new RelayCommand(AddNewClient, IsSelected);
            Remove = new RelayCommand(RemoveClient, IsEnable);
            //Clear = new RelayCommand(ClearClient, IsEnable);
            ClientsRepository = new ClientsRepository(new JsonProvider<Client>("clients.json"));
            Clients= new ObservableCollection<Client>(ClientsRepository.GetAll());
        }

        public ObservableCollection<Client> Clients{ get; set; }
        public ClientsRepository ClientsRepository { get; set; }
        
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                Name = value.Name;
                LastName = value.LastName;
                SecondLastName = value.SecondLastName;
                BDay = value.BDay.ToString();
                NumberDriversLicence = value.NumberDriversLicence;
                OnPropertyChanged();
            }
        }
        public RelayCommand AddNew { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Clear { get; set; }
        private void AddNewClient(object arg)
        {
            var client = new Client() { Name = Name, LastName = LastName, SecondLastName  = SecondLastName, BDay = DateTime.Parse(BDay), NumberDriversLicence = NumberDriversLicence, ManagerID = 0, ID = ClientsRepository.FindMaxIDClient()+1 };
            Clients.Add(client);
            ClearFields();
            ClientsRepository.Add(client);
        }

        private void ClearFields()
        {
            Name = string.Empty;
            LastName = string.Empty;
            SecondLastName = string.Empty;
            BDay = string.Empty;
            NumberDriversLicence = string.Empty;
        }

        private void RemoveClient(object arg)
        {
            if (SelectedClient != null)
            {
                ClientsRepository.Remove(SelectedClient);
                Clients.Remove(SelectedClient);
                ClearFields();
            }

        }

       /* private void ClearClient(object arg)
        {
            if (SelectedClient != null)
            {
                ClearFields();
            }
        }*/
        private bool IsEnable(object value)
        {
            return SelectedClient != null;
        }
        private bool IsSelected(object value)
        {
            ButtonContentClient = SelectedClient != null ? "Edit" : "Add";
            return true;

        }
    }
}
