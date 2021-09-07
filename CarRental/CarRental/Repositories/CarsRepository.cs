using System.Collections.Generic;
using System.Linq;
using CarRental.Providers;

namespace CarRental.Repositories
{
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
    }
}