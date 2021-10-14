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
        private Manager _selectedManager;
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
            Managers = new ObservableCollection<Manager>(ManagersRepository.GetAll());
        }

        public ObservableCollection<Manager> Managers{ get; set; }
        public ManagersRepository ManagersRepository { get; set; }

        public Manager SelectedManager
        {
            get => _selectedManager;
            set
            {
                _selectedManager = value;
                Name = value.Name;
                LastName = value.LastName;
                SecondLastName = value.SecondLastName;
                BDay = value.BDay.ToString();
                OnPropertyChanged();
            }
        }
        public RelayCommand AddNew { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Clear { get; set; }
        private void AddNewManager(object arg)
        {
            var manager= new Manager() { Name = Name, LastName = LastName, SecondLastName = SecondLastName, BDay = DateTime.Parse(BDay), ID = ManagersRepository.FindMaxIDManagers() + 1 };
            Managers.Add(manager);
            ClearFields();
            ManagersRepository.Add(manager);
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
                ManagersRepository.Remove(SelectedManager);
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
    }
}
