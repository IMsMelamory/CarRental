﻿using System.Collections.Generic;
using System.Linq;
using CarRental.Providers;

namespace CarRental.Repositories
{
    public class ManagersRepository : Repository<Manager>
    {
        public ManagersRepository(BaseDataProvider<Manager> jsonProvider) : base(jsonProvider)
        {
        }
        public void RemoveByLastNameManager(string lastname)
        {
            var idToRemove = _entities.FirstOrDefault(x => x.LastName == lastname)?.ID;
            if (idToRemove != null)
            {
                RemoveById(idToRemove.Value);
            }
        }
        public List<Manager> FindByLastName(string lastName)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.LastName == lastName).ToList();
        }
    }
    public class ClientsRepository : Repository<Client>
    {
        public ClientsRepository(BaseDataProvider<Client> jsonProvider) : base(jsonProvider)
        {
        }

        public List<Client> FindByLastName(string lastName)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.LastName == lastName).ToList();
        }

        public List<Client> FindByName(string name)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.Name == name).ToList();
        }
        public List<Client> FindBySecondLastName(string secondlastname)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.SecondLastName == secondlastname).ToList();
        }
        public List<Client> FindByNumberDriverLicense(string numberdriverlicense)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.NumberDriversLicence == numberdriverlicense).ToList();
        }
        public void RemoveByNumberDriversLicense(string number)
        {
            var idToRemove = _entities.FirstOrDefault(x => x.NumberDriversLicence == number)?.ID;
            if (idToRemove != null)
            {
                RemoveById(idToRemove.Value);
            }
        }
    }
    public class CarsRepository : Repository<Car>
    {
        public CarsRepository(BaseDataProvider<Car> jsonProvider) : base(jsonProvider)
        {
        }

        public void RemoveByCarNumber(string number)
        {
            var idToRemove = _entities.FirstOrDefault(x => x.Number == number)?.ID;
            if (idToRemove != null)
            {
                RemoveById(idToRemove.Value);
            }
        }
        public List<Car> FindByNumberCar(string numbercar)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.Number == numbercar).ToList();
        }
        public List<Car> FindByModelCar(string modelcar)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.Model == modelcar).ToList();
        }
        public List<Car> FindByColorCar(string colorcar)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.Color == colorcar).ToList();
        }
        public List<Car> FindByDateRelease(string date)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.DateRelease == date).ToList();
        }
        public List<Car> FindByAviability(bool aviability)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.Availability == aviability).ToList();
        }
    }
}