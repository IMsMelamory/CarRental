using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarRentalCore.Model;
using CarRentalDesktop.Annotations;

namespace CarRentalDesktop.ViewModel
{
    public class ClientsMapper : ClientViewModel
    {
        public List<ClientViewModel> ToViewModel(List<Client> clients)
        {
          return clients.Select(x => new ClientViewModel()
          {
             Name = x.Name,
             LastName = x.LastName,
             SecondLastName = x.SecondLastName,
             BDay = x.BDay,
             NumberDriversLicence = x.NumberDriversLicence,
             ManagerID = x.ManagerID,
             ID = x.ID
          }).ToList();
        }
        public Client ToClient(ClientViewModel vmClient)
        {
            return  new Client()
            {
                Name = vmClient.Name,
                LastName = vmClient.LastName,
                SecondLastName = vmClient.SecondLastName,
                BDay = vmClient.BDay,
                NumberDriversLicence = vmClient.NumberDriversLicence,
                ManagerID = vmClient.ManagerID,
                ID = vmClient.ID
            };
        }
    }
}
