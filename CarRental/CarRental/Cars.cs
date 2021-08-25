using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CarRental
{
    public class Cars
    {
        public string NumberCar { get; set; }
        public string ModelCar { get; set; }
        public string ColorCar { get; set; }
        public string DateRelease { get; set; }
        public bool Availability { get; set; }
        public int DayPrice { get; set; }

        public void AddCar(string numbercar, string modelcar, string colorcar, string daterelease, string availability, int dayprice)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            var jsonData = "";
            if (File.Exists(@"C:\rentcar\cars.json"))
            {
                jsonData = File.ReadAllText(@"C:\rentcar\cars.json");
            }
            else
            {
                Directory.CreateDirectory(@"C:\rentcar");
                var FileCar = File.Create(@"C:\rentcar\c.json");
                FileCar.Close();

            }
            var myCars = JsonConvert.DeserializeObject<List<Cars>>(jsonData)
                            ?? new List<Cars>();
            var car = new Cars()
            {
                NumberCar = numbercar, ModelCar = modelcar, ColorCar = colorcar, DateRelease = daterelease, Availability = bool.Parse(availability), DayPrice = dayprice
            };
            myCars.Add(car);
            File.WriteAllText(@"C:\test\path_car.json", JsonConvert.SerializeObject(myCars, Formatting.Indented, settings));

        }
        public void RemoveCar(string numbercar)
        {
            if (File.Exists(@"C:\rentcar\clients.json"))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };
                var myCars = JsonConvert.DeserializeObject<List<Cars>>(File.ReadAllText(@"C:\test\path_car.json"), settings)
                                ?? new List<Cars>();
                var deleteCar = myCars.Find(car => car.NumberCar == numbercar);
                myCars.Remove(deleteCar);
                File.WriteAllText(@"C:\test\path_car.json", JsonConvert.SerializeObject(myCars, Formatting.Indented, settings));
            }
            else
            {
                Console.WriteLine("Не существует машин! Сначала добавьте");
            }

           
        }

    }
}
