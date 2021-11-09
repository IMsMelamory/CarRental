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
        private string _bDay;
        private DateTime _day;
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

        public ManagersViewModel()
        {
            AddNew = new RelayCommand(AddNewManager);
            Remove = new RelayCommand(RemoveManager, RemoveManager => SelectedManager != null);
            Save = new RelayCommand(SaveManager, SaveManager => SelectedManager != null);
            Clear = new RelayCommand(ClearManager);
            ManagersRepository = new ManagersRepository(new JsonProvider<Manager>("managers.json"));
            Managers = new ObservableCollection<ManagerViewModel>(ManagerMap.ToViewModel(ManagersRepository.GetAll()));
        }

        public ObservableCollection<ManagerViewModel> Managers { get; set; }
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
                    BDay = value.BDay.ToString();
                }
                OnPropertyChanged();
            }
        }
        public RelayCommand AddNew { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Clear { get; set; }
        public RelayCommand Save { get; set; }
        private void AddNewManager(object arg)
        {
            if (DateTime.TryParse(BDay, out _day) == false)
            {
                MessageBox.Show("Введите полную дату рождения");
            }
            else
            {
                var manager = new ManagerViewModel()
                {
                    Name = Name,
                    LastName = LastName,
                    SecondLastName = SecondLastName,
                    BDay = _day,
                    ID = ManagersRepository.FindMaxIDManagers() + 1,
                };
                Managers.Add(manager);
                ClearFields();
                ManagersRepository.Add(ManagerMap.ToManager(manager));
            } 
        }

        private void ClearFields()
        {
            Name = string.Empty;
            LastName = string.Empty;
            SecondLastName = string.Empty;
            BDay = string.Empty;
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
        private void SaveManager(object arg)
        {
            if (SelectedManager != null)
            {
                var myManager = ManagersRepository.FindByID(SelectedManager.ID);
                myManager.Name = Name;
                myManager.LastName = LastName;
                myManager.SecondLastName = SecondLastName;
                ManagersRepository.ForceUpdate();
                SelectedManager.Name = Name;
                SelectedManager.LastName = LastName;
                SelectedManager.SecondLastName = SecondLastName;
                ClearFields();
            }
        }
        private void ClearManager(object arg)
         {
            if (SelectedManager != null)
            {
                ClearFields();
            }
         }
        public override string Header => "Managers";
    }
}
