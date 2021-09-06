using System;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualBasic;



namespace CarRental
{
    public class Client : User
    {
        public string NumberDriversLicence { get; set; }
        Manager Manager { get; set; }

        public void ClientAssociateManager(Manager manager)
        {
            Manager = manager;

            if (!manager.clients.Contains(this))
            {
                manager.clients.Add(this);
            }
        }
    }
}
