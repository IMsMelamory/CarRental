using System.Collections.Generic;
using System.Linq;
using CarRentalCore.Model;
using CarRentalCore.Providers;

namespace CarRentalCore.Repositories
{
    public class ClientsRepository : Repository<Client>
    {
        public ClientsRepository(BaseDataProvider<Client> jsonProvider) : base(jsonProvider)
        {
        }

        public int FindMaxIDClient()
        {
            UpdateDataIfNotExist();
            var id = _entities.Select(x => x.ID);
            return id.Any() ? id.Max() : 0;
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
        public List<Client> FindBySecondLastName(string secondLastName)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.SecondLastName == secondLastName).ToList();
        }
        public List<Client> FindByNumberDriverLicense(string numberDriverLicense)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => x.NumberDriversLicence == numberDriverLicense).ToList();
        }
        public void RemoveByNumberDriversLicense(string number)
        {
            var idToRemove = _entities.FirstOrDefault(x => x.NumberDriversLicence == number)?.ID;
            if (idToRemove != null)
            {
                RemoveById(idToRemove.Value);
            }
        }
        public Client FindByID(int ID)
        {
            UpdateDataIfNotExist();
            return _entities.FirstOrDefault(x => x.ID == ID);
        }
    }
}
