using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;

namespace CarRentalDesktop.ViewModel
{
    public class ManagersViewModel: ManagerViewModel
    {
        private string _name;
        private string _lastName;
        private string _secondLastName;
        private string _bDay;
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
        private ManagerViewModel _selectedManager;
        private string _buttonContent;
        public string ButtonContent
        {
            get => _buttonContent;
            set
            {
                _buttonContent = value;
                OnPropertyChanged();
            }
        }
        public ManagersViewModel()
        {
            AddNew = new RelayCommand(AddNewManager, IsSelected);
            Remove = new RelayCommand(RemoveManager, IsEnable);
            //Clear = new RelayCommand(ClearClient, IsEnable);
            ManagersRepository = new ManagersRepository(new JsonProvider<Manager>("managers.json"));
            Managers = new ObservableCollection<ManagerViewModel>(ManagerMap.ToViewModel(ManagersRepository.GetAll()));
        }

        public ObservableCollection<ManagerViewModel> Managers{ get; set; }
        public ManagersRepository ManagersRepository { get; set; }
        public ManagersMapper ManagerMap { get; set; } = new ManagersMapper();

        public ManagerViewModel SelectedManager
        {
            get => _selectedManager;
            set
            {
                _selectedManager = value;
                if (SelectedManager != null)
                {
                    Name = value.Name;
                    LastName = value.LastName;
                    SecondLastName = value.SecondLastName;
                    BDay = BDay;
                }
                OnPropertyChanged();
            }
        }
        public RelayCommand AddNew { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Clear { get; set; }
        private void AddNewManager(object arg)
        {
            var manager= new ManagerViewModel() { Name = Name, LastName = LastName, SecondLastName = SecondLastName, BDay = DateTime.Parse(BDay) };
            Managers.Add(manager);
            ClearFields();
            ManagersRepository.Add(ManagerMap.ToManager(manager));
        }

        private void ClearFields()
        {
            Name = string.Empty;
            LastName = string.Empty;
            SecondLastName = string.Empty;
           // BDay = string.Empty;
        }

        private void RemoveManager(object arg)
        {
            if (SelectedManager != null)
            {
                ManagersRepository.Remove(ManagerMap.ToManager(SelectedManager));
                Managers.Remove(SelectedManager);
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
            return SelectedManager != null;
        }
        private bool IsSelected(object value)
        {
            ButtonContent = SelectedManager != null ? "Edit" : "Add";
            return true;

        }
        public override string Header => "Managers";
    }
}
