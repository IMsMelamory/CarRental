using System;
using System.Collections.ObjectModel;
using System.Windows;
using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;

namespace CarRentalDesktop.ViewModel
{
    public class RentsViewModel: BaseViewModel
    {
        private RentViewModel _selectedRent;
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
        private string _clientNumderLicense;
        private string _carNmber;
        private string _startRent;
        private DateTime _dayStart;
        private string _endRent;
        private DateTime _dayEnd;
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
        public string StartRent
        {
            get => _startRent;
            set
            {          
                _startRent = value;
                OnPropertyChanged();
            }
        }
        public string EndRent
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
        public ClientsViewModel ClientsVM { get; set; }
        public CarsViewModel CarsVM { get; set; }
        public RentsViewModel(ClientsViewModel clientsVM, CarsViewModel carsVM) 
        {
            ClientsVM = clientsVM;
            CarsVM = carsVM;
            AddNew = new RelayCommand(AddNewRent);
            Remove = new RelayCommand(RemoveRent);
            Return = new RelayCommand(ReturnRent);
            RentsRepository = new RentsRepository(new JsonProvider<Rent>("rents.json"));
            Rents = new ObservableCollection<RentViewModel>(RentMap.ToViewModel(RentsRepository.GetAll()));

        }
        public RentsRepository RentsRepository { get; set; }
        public ObservableCollection<RentViewModel> Rents { get; set; }
        public RentsMapper RentMap { get; set; } = new RentsMapper();

        public RelayCommand AddNew { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Return { get; set; }
        private void AddNewRent(object arg)
        {
            if (DateTime.TryParse(StartRent, out _dayStart) == false)
            {
                MessageBox.Show("Введите полную дату начала аренды");
            }
            else
            {
                var rent = new RentViewModel()
                {
                    ClientNumberLicense = ClientNumberLicense,
                    CarNumber = CarNumber,
                    StartRent = _dayStart,
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
            if (DateTime.TryParse(EndRent, out _dayEnd) == false)
            {
                MessageBox.Show("Введите полную дату окончания аренды");
            }
            else
            {
                if (SelectedRent != null)
                {
                    var selectRent = RentMap.ToRent(SelectedRent);
                    RentsRepository.UpdateDateEnd(selectRent.ClientNumberLicense, selectRent.CarNumber, _dayEnd);
                    RentsRepository.ChekIsFine(selectRent.ClientNumberLicense, selectRent.CarNumber, _dayEnd);
                    SelectedRent.EndRent = _dayEnd;
                    ChekFine(SelectedRent);
                }
                ClearFields();
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
                    ClientNumberLicense = value.ClientNumberLicense;
                    CarNumber = value.CarNumber;
                    StartRent = value.StartRent.ToString();
                    EndRent = value.EndRent.ToString();
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
            StartRent = string.Empty;
            EndRent = string.Empty;
            Fine = 0;
            DayRentCount = 0;

        }
        private void RemoveRent(object arg)
        {
            if (SelectedRent != null)
            {
                RentsRepository.Remove(RentMap.ToRent(SelectedRent));
                Rents.Remove(SelectedRent);
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
            return SelectedRent != null;
        }
        private void ChekFine(RentViewModel car)
        {
            if ((car.EndRent - car.StartRent).TotalDays > car.DayRentCount)
            {
                car.Fine = ((car.EndRent - car.StartRent).TotalDays - car.DayRentCount) * 5;
            }
            var k = 0;
        }
        
        public override string Header => "Rents";
    }
}
