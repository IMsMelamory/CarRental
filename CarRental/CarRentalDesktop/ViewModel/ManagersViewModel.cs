using System;
using System.Collections.ObjectModel;
using System.Windows;
using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;

namespace CarRentalDesktop.ViewModel
{
    public class ManagersViewModel : BaseTab
    {
        private string _name;
        private string _lastName;
        private string _secondLastName;
        private DateTime _bDay;
        private ManagerViewModel _selectedManager;

        public ManagersViewModel()
        {
            AddNewManager = new RelayCommand(AddNew, AddNew=> SelectedManager == null);
            RemoveManager = new RelayCommand(Remove, Remove=> SelectedManager != null);
            SaveManager = new RelayCommand(Save, Save => SelectedManager != null);
            Clear = new RelayCommand(ClearManager);
            ManagersRepository = new ManagersRepository(new JsonProvider<Manager>("managers.json"));
            UpdateManagers();
        }

        public ObservableCollection<ManagerViewModel> Managers { get; set; }
        public ManagersRepository ManagersRepository { get; set; }
        public ManagersMapper ManagerMap { get; set; } = new ManagersMapper();
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
        public DateTime BDay
        {
            get => _bDay;
            set
            {

                _bDay = value;
                OnPropertyChanged();
            }
        }

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
                    BDay = value.BDay;
                }
                OnPropertyChanged();
            }
        }
        public RelayCommand AddNewManager { get; set; }
        public RelayCommand RemoveManager { get; set; }
        public RelayCommand Clear { get; set; }
        public RelayCommand SaveManager { get; set; }
        private void UpdateManagers()
        {
            Managers = new ObservableCollection<ManagerViewModel>(ManagerMap.ToViewModel(ManagersRepository.GetAll()));
        }
        private void AddNew(object arg)
        {
           var manager = new ManagerViewModel()
           {
                Name = Name,
                LastName = LastName,
                SecondLastName = SecondLastName,
                BDay = BDay,
                ID = ManagersRepository.FindMaxIDManagers() + 1,
            };
           ManagersRepository.Add(ManagerMap.ToManager(manager));
           UpdateManagers();
           ClearFields();

        }

        private void ClearFields()
        {
            Name = string.Empty;
            LastName = string.Empty;
            SecondLastName = string.Empty;
            BDay = DateTime.MinValue;
        }

        private void Remove(object arg)
        {
            if (SelectedManager == null)
            {
                return;
            }
            ManagersRepository.RemoveById(ManagerMap.ToManager(SelectedManager).ID);
            ClearFields();
            UpdateManagers();
        }
        private void Save(object arg)
        {
            if (SelectedManager == null)
            {
                return;
            }
            var myManager = ManagersRepository.FindByID(SelectedManager.ID);
            myManager.Name = Name;
            myManager.LastName = LastName;
            myManager.SecondLastName = SecondLastName;
            ManagersRepository.ForceUpdate();
            ClearFields();
            UpdateManagers();
        }
        private void ClearManager(object arg)
         {
            if (SelectedManager == null)
            {
                return;
            }
            ClearFields();
            SelectedManager = null;
         }
        public override string Header => "Managers";
    }
}
