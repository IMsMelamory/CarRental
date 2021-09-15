using System.Linq;
using CarRental.Repositories;

namespace CarRental.Services
{
    public class ClientAssociatedService
    {
        private readonly Repository<Client> _clientsRep;
        private readonly Repository<Manager> _managersRep;

        public ClientAssociatedService(Repository<Client> clientsRep, Repository<Manager> managersRep)
        {
            _clientsRep = clientsRep;
            _managersRep = managersRep;

            _clientsRep.OnRepositoryUpdate += SyncAssociate;
            _managersRep.OnRepositoryUpdate += SyncAssociate;
        }

        private void SyncAssociate()
        {
            var clients = _clientsRep.GetAll();
            var managers = _managersRep.GetAll();

            foreach (var manager in managers)
            {
                manager.Clients = clients.Where(x => x.ManagerID == manager.ID).ToList();
                manager.Clients.ForEach(x=>x.Manager = manager);
            }
        }
    }
}