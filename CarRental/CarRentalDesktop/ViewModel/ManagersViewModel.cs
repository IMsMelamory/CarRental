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
    public class ManagersViewModel : BaseTab
    {
        private ManagerViewModel _selectedManager;
        private ManagerViewModel _currentManager;

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
        public ManagerViewModel CurrentManager
        {
            get => _currentManager;
            set
            {
                _currentManager = value;
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
                    CurrentManager = SelectedManager;
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
            Managers = new ObservableCollection<ManagerViewModel>(ManagerMap.ToViewModel(ManagersRepository.GetAll()).OrderBy(x => x.ID));
        }
        private void AddNew(object arg)
        {
          /* var manager = new ManagerViewModel()
           {
                Name = Name,
                LastName = LastName,
                SecondLastName = SecondLastName,
                BDay = BDay,
                ID = ManagersRepository.FindMaxIDManagers() + 1,
            };
           ManagersRepository.Add(ManagerMap.ToManager(manager));
           UpdateManagers();
           ClearFields();*/

        }

        private void ClearFields()
        {
            CurrentManager.Name = string.Empty;
            CurrentManager.LastName = string.Empty;
            CurrentManager.SecondLastName = string.Empty;
            CurrentManager.BDay = DateTime.MinValue;
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
            ManagersRepository.EditById(SelectedManager.ID, ManagerMap.ToManager(CurrentManager));
            UpdateManagers();
            ClearFields();
            
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
