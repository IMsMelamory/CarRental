using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalDesktop.ViewModel;

namespace CarRentalDesktop.Model
{
    public class Car: BaseViewModel
    {
        public string Number { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string DateRelease { get; set; }
        public string DayPrice { get; set; }
    }

}
