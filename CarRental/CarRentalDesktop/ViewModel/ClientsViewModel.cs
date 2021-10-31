using System;
using System.Collections.ObjectModel;
using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;

namespace CarRentalDesktop.ViewModel
{
    public class ClientsViewModel: BaseViewModel
    {
        private string _name;
        private string _lastName;
        private string _secondLastName;
        private string _bDay;
        private string _numberDriversLicence;
        private int _managerID;
        private ClientViewModel _selectedClient;
        private string _buttonContentClient;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        public string SecondLastName
        {
            get => _secondLastName;
            set
            {
                _secondLastName = value;
                OnPropertyChanged();
            }
        }
        public string BDay
        {
            get => _bDay;
            set
            {
                _bDay = value;
                OnPropertyChanged();
            }
        }
        public string NumberDriversLicence
        {
            get => _numberDriversLicence;
            set
            {
                _numberDriversLicence = value;
                OnPropertyChanged();
            }
        }
        public int ManagerID
        {
            get => _managerID;
            set
            {
                _managerID = value;
                OnPropertyChanged();
            }
        }
       
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
            Clients = new ObservableCollection<ClientViewModel>(ClientMap.ToViewModel(ClientsRepository.GetAll()));
        }
        private ObservableCollection<ClientViewModel> _clients;
        public ObservableCollection<ClientViewModel> Clients
        {
            get => _clients;

            set{_clients = value; 
                OnPropertyChanged(); }
        }
        public ClientsRepository ClientsRepository { get; set; }
        public ClientsMapper ClientMap { get; set; } = new ClientsMapper();
        
        public ClientViewModel SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                if (SelectedClient != null)
                {
                    Name = value.Name;
                    LastName = value.LastName;
                    SecondLastName = value.SecondLastName;
                    BDay = value.BDay.ToString();
                    NumberDriversLicence = value.NumberDriversLicence;
                }
                OnPropertyChanged();
            }
        }
        public CarsViewModel S { get; set; }
        public RelayCommand AddNew { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Clear { get; set; }
        private void AddNewClient(object arg)
        {
            var client = new ClientViewModel() { Name = Name, LastName = LastName, SecondLastName  = SecondLastName, BDay = DateTime.Parse(BDay), NumberDriversLicence = NumberDriversLicence, ManagerID = 0 };
            Clients.Add(client);
            ClearFields();
            ClientsRepository.Add(ClientMap.ToClient(client));

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
                ClientsRepository.Remove(ClientMap.ToClient(SelectedClient));
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
        public override string Header => "Clients";
    }
}
