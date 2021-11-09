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
        private string _name;
        private string _lastName;
        private string _secondLastName;
        private string _bDay;
        private DateTime _day;
        private string _numberDriversLicence;
        private int _managerID;
        private ClientViewModel _selectedClient;
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
        public ManagersViewModel ManagerVM { get; set; }
        public ClientsViewModel(ManagersViewModel managerVM)
        {
            ManagerVM = managerVM;
            AddNew = new RelayCommand(AddNewClient);
            Remove = new RelayCommand(RemoveClient, RemoveClient => SelectedClient != null);
            Save = new RelayCommand(SaveClient, SaveClient => SelectedClient != null);
            //Clear = new RelayCommand(ClearClient, IsEnable);
            ClientsRepository = new ClientsRepository(new JsonProvider<Client>("clients.json"));
            Clients = new ObservableCollection<ClientViewModel>(ClientMap.ToViewModel(ClientsRepository.GetAll()));
        }
        private ObservableCollection<ClientViewModel> _clients;
        public ObservableCollection<ClientViewModel> Clients
        {
            get => _clients;

            set
            {
                _clients = value;
                OnPropertyChanged();}
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
                    ManagerID = value.ManagerID;
                }
                OnPropertyChanged();
            }
        }
        public CarsViewModel S { get; set; }
        public RelayCommand AddNew { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Clear { get; set; }
        public RelayCommand Save { get; set; }
        private void AddNewClient(object arg)
        {
            if (DateTime.TryParse(BDay, out _day) == false)
            {
                MessageBox.Show("Введите полную дату рождения");
            }
            else
            {
                var client = new ClientViewModel()
                {
                    Name = Name,
                    LastName = LastName,
                    SecondLastName = SecondLastName,
                    BDay = _day,
                    NumberDriversLicence = NumberDriversLicence,
                    ID = ClientsRepository.FindMaxIDClient() + 1,
                    ManagerID = ManagerID
                };
                Clients.Add(client);
                ClearFields();
                ClientsRepository.Add(ClientMap.ToClient(client));
            }
        }

        private void ClearFields()
        {
            Name = string.Empty;
            LastName = string.Empty;
            SecondLastName = string.Empty;
            BDay = string.Empty;
            NumberDriversLicence = string.Empty;
            ManagerID = 0;
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
        private void SaveClient(object arg)
        {
            if (SelectedClient != null)
            {
                var myClient = ClientsRepository.FindByID(SelectedClient.ID);
                myClient.Name = Name;
                myClient.LastName = LastName;
                myClient.SecondLastName = SecondLastName;
                myClient.NumberDriversLicence = NumberDriversLicence;
                myClient.ManagerID = ManagerID;
                ClientsRepository.ForceUpdate();
                SelectedClient.Name = Name;
                SelectedClient.LastName = LastName;
                SelectedClient.SecondLastName = SecondLastName;
                SelectedClient.NumberDriversLicence = NumberDriversLicence;
                SelectedClient.ManagerID = ManagerID;
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
        public override string Header => "Clients";
    }
}
