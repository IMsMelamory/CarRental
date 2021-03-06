using System.Collections.Generic;
using System.Linq;
using CarRentalCore.Model;
using CarRentalCore.Providers;

namespace CarRentalCore.Repositories
{
    public class CarsRepository : Repository<Car>
    {
        public CarsRepository(BaseDataProvider<Car> jsonProvider) : base(jsonProvider)
        {
        }
        public int FindMaxIDCar()
        {
            UpdateDataIfNotExist();
            var id = _entities.Select(x => x.ID);
            return id.Any() ? id.Max() : 0;


        }

        public void RemoveByCarNumber(string number)
        {
            var idToRemove = _entities.FirstOrDefault(x => x.Number == number)?.ID;
            if (idToRemove != null)
            {
                RemoveById(idToRemove.Value);
            }
        }
        public List<Car> FindByNumberCar(string numberCar)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.Number == numberCar).ToList();
        }
        public List<Car> FindByModelCar(string modelCar)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.Model == modelCar).ToList();
        }
        public List<Car> FindByColorCar(string colorCar)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.Color == colorCar).ToList();
        }
        public List<Car> FindByDateRelease(string date)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.DateRelease == date).ToList();
        }
        public Car FindByID(int ID)
        {
            UpdateDataIfNotExist();
            return _entities.FirstOrDefault(x => x.ID == ID);
        }
    }
}