using System.Collections.Generic;
using System.Linq;
using CarRentalCore.Model;

namespace CarRentalDesktop.ViewModel
{
    public class ManagersMapper : ManagerViewModel
    {
        public List<ManagerViewModel> ToViewModel(List<Manager> managers)
        {
          return managers.Select(x => new ManagerViewModel()
          {
             Name = x.Name,
             LastName = x.LastName,
             SecondLastName = x.SecondLastName,
             BDay = x.BDay,
             ID = x.ID,
          }).ToList();
        }
        public Manager ToManager(ManagerViewModel vmManager)
        {
            return new Manager()
            {
                Name = vmManager.Name,
                LastName = vmManager.LastName,
                SecondLastName = vmManager.SecondLastName,
                BDay = vmManager.BDay,
                ID = vmManager.ID
            };
        }
    }
}
