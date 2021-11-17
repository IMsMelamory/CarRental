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
        public override string Header => "Managers";
        private ManagerViewModel _selectedManager;
        private ManagerViewModel _currentManager = new ManagerViewModel();

        public ManagersViewModel()
        {
            AddNewManagerCommand = new RelayCommand(AddNewExecute, AddNew=> SelectedManager == null);
            RemoveManagerCommand = new RelayCommand(RemoveExecute, Remove=> SelectedManager != null);
            SaveManagerCommand = new RelayCommand(SaveExecute, Save => SelectedManager != null);
            ClearCommand = new RelayCommand(ClearManagerExecute);
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
        public RelayCommand AddNewManagerCommand { get; set; }
        public RelayCommand RemoveManagerCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand SaveManagerCommand { get; set; }
        private void UpdateManagers()
        {
            Managers = new ObservableCollection<ManagerViewModel>(ManagerMap.ToViewModel(ManagersRepository.GetAll()).OrderBy(x => x.ID));
        }
        private void AddNewExecute(object arg)
        {
           CurrentManager.ID = ManagersRepository.FindMaxIDManagers() + 1;
           ManagersRepository.Add(ManagerMap.ToManager(CurrentManager));
           UpdateManagers();
           ClearFields();

        }

        private void ClearFields()
        {
            CurrentManager = new ManagerViewModel();
        }

        private void RemoveExecute(object arg)
        {
            if (SelectedManager == null)
            {
                return;
            }
            ManagersRepository.RemoveById(ManagerMap.ToManager(SelectedManager).ID);
            ClearFields();
            UpdateManagers();
        }
        private void SaveExecute(object arg)
        {
            if (SelectedManager == null)
            {
                return;
            }
            ManagersRepository.EditById(SelectedManager.ID, ManagerMap.ToManager(CurrentManager));
            UpdateManagers();
            ClearFields();
            
        }
        private void ClearManagerExecute(object arg)
         {
            if (SelectedManager == null)
            {
                return;
            }
            ClearFields();
            SelectedManager = null;
         }
    }
}
