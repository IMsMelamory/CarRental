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
        private RentViewModel _currentRent;

        public RentsViewModel(ClientsViewModel clientsVM, CarsViewModel carsVM)
        {
            ClientsVM = clientsVM;
            CarsVM = carsVM;
            AddNewCommand = new RelayCommand(AddNewRentExecute, AddNewRent => SelectedRent == null);
            RemoveCommand = new RelayCommand(RemoveRentExecute, RemoveRent => SelectedRent != null);
            ReturnCommand = new RelayCommand(ReturnRentExecute, ReturnRent => SelectedRent != null);
            ClearCommand = new RelayCommand(ClearRentExecute);
            RentsRepository = new RentsRepository(new JsonProvider<Rent>("rents.json"));
            UpdateRents();

        }
        public ClientsViewModel ClientsVM { get; set; }
        public CarsViewModel CarsVM { get; set; }
        public RentsRepository RentsRepository { get; set; }
        public ObservableCollection<RentViewModel> Rents { get; set; }
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
        public RelayCommand AddNewCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand ReturnCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
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
               /* var rent = new RentViewModel()
                {
                    CurrentRent.ID
                    ClientNumberLicense = ClientNumberLicense,
                    CarNumber = CarNumber,
                    StartRent = StartRent,
                    EndRent = DateTime.MinValue,
                    Fine = 0,
                    DayRentCount = DayRentCount
                };
                Rents.Add(rent);
                ClearFields();
                RentsRepository.Add(RentMap.ToRent(rent));*/
            }
        }
        private void ReturnRentExecute(object arg)
        {
                if (SelectedRent != null)
                {
                    var selectRent = RentMap.ToRent(SelectedRent);
                    RentsRepository.UpdateDateEnd(selectRent.ClientNumberLicense, selectRent.CarNumber, CurrentRent.EndRent);
                    RentsRepository.ChekIsFine(selectRent.ClientNumberLicense, selectRent.CarNumber, CurrentRent.EndRent);
                    SelectedRent.EndRent = CurrentRent.EndRent;
                    ChekFine(SelectedRent);
                }
                ClearFields();
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

        private void ClearFields()
        {
            CurrentRent.ClientNumberLicense = string.Empty;
            CurrentRent.CarNumber = string.Empty;
            CurrentRent.StartRent = DateTime.MinValue;
            CurrentRent.EndRent = DateTime.MinValue;
            CurrentRent.Fine = 0;
            CurrentRent.DayRentCount = 0;

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
