using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CarRental
{
    public class Car
    {
        public string Number { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string DateRelease { get; set; }
        public bool Availability { get; set; }
        public int DayPrice { get; set; }
    }
}
