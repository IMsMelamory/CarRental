using System.Collections.Generic;
using System.Linq;
using System;
using CarRental.Providers;

namespace CarRental.Repositories
{
    public class ManagersRepository : Repository<Manager>
    {
        public ManagersRepository(BaseDataProvider<Manager> jsonProvider) : base(jsonProvider)
        {
        }
        public void RemoveByLastNameManager(string lastName)
        {
            var idToRemove = _entities.FirstOrDefault(x => x.LastName == lastName)?.ID;
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
}
