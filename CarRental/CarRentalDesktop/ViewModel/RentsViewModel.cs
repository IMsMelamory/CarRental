using System;
using System.Collections.ObjectModel;
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
        private string _clientNumderLicense;
        private string _carNmber;
        private DateTime _startRent;
        private DateTime _endRent;
        private int _dayRentCount;
        private double _fine;
        public string ClientNumberLicense
        {
            get => _clientNumderLicense;
            set
            {
                _clientNumderLicense = value;
                OnPropertyChanged();
            }
        }
        public string CarNumber
        {
            get => _carNmber;
            set
            {
                _carNmber = value;
                OnPropertyChanged();
            }
        }
        public DateTime StartRent
        {
            get => _startRent;
            set
            {          
                _startRent = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndRent
        {
            get => _endRent;
            set
            {
                _endRent = value;
                OnPropertyChanged();
            }
        }
        public int DayRentCount
        {
            get => _dayRentCount;
            set
            {
                _dayRentCount = value;
                OnPropertyChanged();
            }
        }
        public double Fine
        {
            get => _fine;
            set
            {
                _fine = value;
                OnPropertyChanged();
            }
        }
        public RentsViewModel(ClientsViewModel clientsVM, CarsViewModel carsVM)
        {
            ClientsVM = clientsVM;
            CarsVM = carsVM;
            AddNew = new RelayCommand(AddNewRent, AddNewRent => SelectedRent == null);
            Remove = new RelayCommand(RemoveRent, RemoveRent => SelectedRent != null);
            Return = new RelayCommand(ReturnRent, ReturnRent => SelectedRent != null);
            Clear = new RelayCommand(ClearRent);
            RentsRepository = new RentsRepository(new JsonProvider<Rent>("rents.json"));
            UpdateRents();

        }
        public ClientsViewModel ClientsVM { get; set; }
        public CarsViewModel CarsVM { get; set; }
        public RentsRepository RentsRepository { get; set; }
        public ObservableCollection<RentViewModel> Rents { get; set; }
        public RentsMapper RentMap { get; set; } = new RentsMapper();

        public RelayCommand AddNew { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Return { get; set; }
        public RelayCommand Clear { get; set; }
        private void UpdateRents()
        {
            Rents = new ObservableCollection<RentViewModel>(RentMap.ToViewModel(RentsRepository.GetAll()));
        }
        private void AddNewRent(object arg)
        {
            if (DayRentCount <= 0)
            {
                MessageBox.Show("Количество дней аренды должно быть >0");
            }
            else
            {
                var rent = new RentViewModel()
                {
                    ClientNumberLicense = ClientNumberLicense,
                    CarNumber = CarNumber,
                    StartRent = StartRent,
                    EndRent = DateTime.MinValue,
                    Fine = 0,
                    DayRentCount = DayRentCount
                };
                Rents.Add(rent);
                ClearFields();
                RentsRepository.Add(RentMap.ToRent(rent));
                }
        }
        private void ReturnRent(object arg)
        {
                if (SelectedRent != null)
                {
                    var selectRent = RentMap.ToRent(SelectedRent);
                    RentsRepository.UpdateDateEnd(selectRent.ClientNumberLicense, selectRent.CarNumber, EndRent);
                    RentsRepository.ChekIsFine(selectRent.ClientNumberLicense, selectRent.CarNumber, EndRent);
                    SelectedRent.EndRent = EndRent;
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
                    ClientNumberLicense = value.ClientNumberLicense;
                    CarNumber = value.CarNumber;
                    StartRent = value.StartRent;
                    EndRent = value.EndRent;
                    Fine = value.Fine;
                    DayRentCount = value.DayRentCount;
                }
                OnPropertyChanged();
            }
        }

        private void ClearFields()
        {
            ClientNumberLicense = string.Empty;
            CarNumber = string.Empty;
            StartRent = DateTime.MinValue;
            EndRent = DateTime.MinValue;
            Fine = 0;
            DayRentCount = 0;

        }
        private void RemoveRent(object arg)
        {
            if (SelectedRent == null)
            {
                return;
            }
            RentsRepository.RemoveById(RentMap.ToRent(SelectedRent).ID);
            ClearFields();
            UpdateRents();
        }

        private void ClearRent(object arg)
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
