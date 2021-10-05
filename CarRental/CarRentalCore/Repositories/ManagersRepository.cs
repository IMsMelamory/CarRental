using System.Linq;
using CarRentalCore.Model;
using CarRentalCore.Providers;

namespace CarRentalCore.Repositories
{
    public class ManagersRepository : Repository<Manager>
    {
        public ManagersRepository(BaseDataProvider<Manager> jsonProvider) : base(jsonProvider)
        {
        }
        public int FindMaxIDManagers()
        {
            UpdateDataIfNotExist();
            var id = _entities.Select(x => x.ID);
            if (id.Any())
            {
                return id.Max();
            }
            else
            {
                return 0;
            }
        }

        public void RemoveByLastNameManager(string lastName)
        {
            var idToRemove = _entities.FirstOrDefault(x => x.LastName == lastName)?.ID;
            if (idToRemove != null)
            {
                RemoveById(idToRemove.Value);
            }
            
        }
        public Manager FindByLastName(string lastName)
        {
            UpdateDataIfNotExist();
            return _entities.FirstOrDefault(x => x.LastName == lastName);
        }
    }
}
