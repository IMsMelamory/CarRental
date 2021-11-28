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
    public class RentsViewModel: BaseTab
    {
        private RentViewModel _selectedRent;
        private RentViewModel _currentRent = new RentViewModel(); 
        private ObservableCollection<RentViewModel> _rents;

        public RentsViewModel(ClientsViewModel clientsVM, CarsViewModel carsVM)
        {
            ClientsVM = clientsVM;
            CarsVM = carsVM;
            AddNewRentCommand = new RelayCommand(AddNewRentExecute, AddNewRent => SelectedRent == null);
            RemoveRentCommand = new RelayCommand(RemoveRentExecute, RemoveRent => SelectedRent != null);
            ReturnRentCommand = new RelayCommand(ReturnRentExecute, ReturnRent => SelectedRent != null);
            ClearRentCommand = new RelayCommand(ClearRentExecute);
            RentsRepository = new RentsRepository(new JsonProvider<Rent>("rents.json"));
            UpdateRents();

        }
        public ClientsViewModel ClientsVM { get; set; }
        public CarsViewModel CarsVM { get; set; }
        public RentsRepository RentsRepository { get; set; }
        public ObservableCollection<RentViewModel> Rents
        {
            get => _rents;
            set
            {
                _rents = value;
                OnPropertyChanged();
            }
        }
        public RentsMapper RentMap { get; set; } = new RentsMapper();
        public RentViewModel CurrentRent
        {
            get => _currentRent;
            set
            {
                _currentRent = value;
                OnPropertyChanged();
            }
        }
        public RentViewModel SelectedRent
        {
            get => _selectedRent;
            set
            {
                _selectedRent = value;
                if (SelectedRent != null)
                {
                    CurrentRent = SelectedRent;
                }
                OnPropertyChanged();
            }
        }
        public RelayCommand AddNewRentCommand { get; set; }
        public RelayCommand RemoveRentCommand { get; set; }
        public RelayCommand ReturnRentCommand { get; set; }
        public RelayCommand ClearRentCommand { get; set; }
        private void UpdateRents()
        {
            Rents = new ObservableCollection<RentViewModel>(RentMap.ToViewModel(RentsRepository.GetAll()));
        }
        private void AddNewRentExecute(object arg)
        {
           if (CurrentRent.DayRentCount <= 0)
            {
                MessageBox.Show("Количество дней аренды должно быть >0");
            }
            else
            {
                CurrentRent.ID = RentsRepository.FindMaxIDRent() + 1;
                RentsRepository.Add(RentMap.ToRent(CurrentRent));
                UpdateRents();
                ClearFields();
            }
        }
        private void ReturnRentExecute(object arg)
        {
            if (SelectedRent == null)
            {
                return;
            }
            var selectRent = RentMap.ToRent(SelectedRent);
            RentsRepository.UpdateDateEnd(selectRent.ClientNumberLicense, selectRent.CarNumber, CurrentRent.EndRent);
            RentsRepository.ChekIsFine(selectRent.ClientNumberLicense, selectRent.CarNumber, CurrentRent.EndRent);
            SelectedRent.EndRent = CurrentRent.EndRent;
            ChekFine(SelectedRent);
            ClearFields();
        }

        private void ClearFields()
        {
            CurrentRent = new RentViewModel();

        }
        private void RemoveRentExecute(object arg)
        {
            if (SelectedRent == null)
            {
                return;
            }
            RentsRepository.RemoveById(RentMap.ToRent(SelectedRent).ID);
            ClearFields();
            UpdateRents();
        }

        private void ClearRentExecute(object arg)
         {
            if (SelectedRent == null)
            {
              return;
            }
            ClearFields();
            SelectedRent = null;
        }
        private void ChekFine(RentViewModel car)
        {
            var dayCount = (car.EndRent - car.StartRent).Days ;
            if (dayCount > car.DayRentCount)
            {
                car.Fine = (dayCount - car.DayRentCount) * 5;
            }
        }
        public override string Header => "Rents";
    }
}
