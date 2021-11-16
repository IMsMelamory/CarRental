using System;
using CarRentalCore.Model;
using CarRentalCore.Providers;
using CarRentalCore.Repositories;
using CarRentalDesktop.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace CarRentalDesktop.ViewModel
{
    public class CarsViewModel : BaseTab
    {
        private CarViewModel _selectedCar;
        private CarViewModel _currentCar = new CarViewModel();
        private ObservableCollection<CarViewModel> _cars = new ObservableCollection<CarViewModel>();

        public CarsViewModel()
        {
            AddNewCarCommand = new RelayCommand(AddNewExecute, addNew => SelectedCar == null);
            RemoveCarCommand = new RelayCommand(RemoveExecute, removeCar => SelectedCar != null);
            ClearCommand = new RelayCommand(ClearCarExecute);
            SaveCarCommand = new RelayCommand(SaveExecute, saveCar => SelectedCar != null);
            CarRepository = new CarsRepository(new JsonProvider<Car>("cars.json"));
            UpdateCars();
        }

        public ObservableCollection<CarViewModel> Cars
        {
            get => _cars;
            set
            {
                _cars = value;
                OnPropertyChanged();
            }
        }

        public CarsRepository CarRepository { get; set; }
        public CarsMapper CarMapper { get; set; } = new CarsMapper();

        public CarViewModel SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                if (SelectedCar != null)
                {
                    CurrentCar = SelectedCar;
                }

                OnPropertyChanged();
            }
        }

        public CarViewModel CurrentCar
        {
            get => _currentCar;
            set
            {
                _currentCar = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddNewCarCommand { get; set; }
        public RelayCommand RemoveCarCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand SaveCarCommand { get; set; }

        public override string Header => "Cars";

        private void UpdateCars()
        {
            Cars = new ObservableCollection<CarViewModel>(CarMapper.ToViewModel(CarRepository.GetAll())
                .OrderBy(x => x.ID));
        }

        private void AddNewExecute(object arg)
        {
            CurrentCar.ID = CarRepository.FindMaxIDCar() + 1;
            try
            {
                CarRepository.Add(CarMapper.ToCar(CurrentCar));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
            UpdateCars();
            ClearFields();
        }

        private void ClearFields()
        {
            CurrentCar = new CarViewModel();
        }

        private void RemoveExecute(object arg)
        {
            if (SelectedCar == null)
            {
                return;
            }

            CarRepository.RemoveById(SelectedCar.ID);
            ClearFields();
            UpdateCars();
        }

        private void SaveExecute(object arg)
        {
            if (SelectedCar == null)
            {
                return;
            }

            if (CurrentCar.DayPrice <= 0)
            {
                MessageBox.Show("Цена аренды должна быть > 0");
            }
            else
            {
                CarRepository.EditById(SelectedCar.ID, CarMapper.ToCar(CurrentCar));
                ClearFields();
                UpdateCars();
            }
        }

        private void ClearCarExecute(object arg)
        {
            ClearFields();
            SelectedCar = null;
        }
    }
}