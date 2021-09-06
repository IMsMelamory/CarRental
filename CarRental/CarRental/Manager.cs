using System;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;


namespace CarRental
{
    public class Manager: User
    {
        public List<Client> clients { get; set; } = new List<Client>();

        public void AssociateManager( Client client)
        {
                client.ClientAssociateManager(this);
        }
    }

  
}
