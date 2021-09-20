using System;
using System.Collections.Generic;
using System.Linq;
using CarRental.Repositories;

namespace CarRental.Services
{
    class ClientManagerAssociateService
    {
        private readonly Repository<Client> _clientsRepository;
        private readonly Repository<Manager> _managersRepository;

        public ClientManagerAssociateService(Repository<Client> clientsRep, Repository<Manager> managersRep)
        {
            _clientsRepository = clientsRep;
            _managersRepository = managersRep;

            _clientsRepository.OnRepositoryUpdate += SyncAssociate;
            _managersRepository.OnRepositoryUpdate += SyncAssociate;
        }

        private void SyncAssociate()
        {
            var clients = _clientsRepository.GetAll();
            var managers = _managersRepository.GetAll();

            foreach (var manager in managers)
            {
                manager.Clients = clients.Where(x => x.ManagerID == manager.ID).ToList();
                manager.Clients.ForEach(x => x.Manager = manager);
            }
        }
    }
}
