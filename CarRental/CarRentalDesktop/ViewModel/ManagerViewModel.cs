﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDesktop.ViewModel
{
    public class ManagerViewModel: BaseViewModel
    {
        private string _name;
        private string _lastName;
        private string _secondLastName;
        private string _bDay;
        private int _id;
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
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
    }
}
