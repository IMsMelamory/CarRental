using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repozitory
{
    public class CarRepository: BaseRepository
    {
        public static void AddCar(string numbercar, string modelcar, string colorcar, string daterelease, string availability, int dayprice)
        {

            var filepath = "cars.json";
            var myCars = GetAll<Car>(filepath);
            myCars.Add(new Car()
            {
                Number = numbercar,
                Model = modelcar,
                Color = colorcar,
                DateRelease = daterelease,
                Availability = bool.Parse(availability),
                DayPrice = dayprice
            });
            WriteAll<Car>(filepath, myCars);

        }
        public static void RemoveCar(string numbercar)
        {
            if (File.Exists(@"C:\rentcar\clients.json"))
            {
                var filepath = "cars.json";
                var myCars = GetAll<Car>(filepath);
                myCars.Remove(myCars.Find(car => car.Number == numbercar));
                WriteAll<Car>(filepath, myCars);
            }
            else
            {
                Console.WriteLine("Не существует машин! Сначала добавьте");
            }

        }
        public static List<Car> GetCars()
        {
            var filepath = "cars.json";
            var myCars = GetAll<Car>(filepath);
            return myCars;
        }

    }
}
