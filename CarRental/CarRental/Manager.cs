using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
