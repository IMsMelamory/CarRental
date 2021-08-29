using System;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;


namespace CarRental
{
    public class Client : User
    {
        public string NumberDriversLicence { get; set; }
        Manager Manager { get; set; }

        public void AddClient(string lastName, string name, string secondLastName, DateTime bDay, string numberDriverLicense, Manager manager)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            var jsonData = "";
            if (File.Exists(@"C:\rentcar\clients.json"))
            {
                jsonData = File.ReadAllText(@"C:\rentcar\clients.json");
            }
            else
            {
                Directory.CreateDirectory(@"C:\rentcar");
                var FileCar = File.Create(@"C:\rentcar\clients.json");
                FileCar.Close();

            }
            var myClients = JsonConvert.DeserializeObject<List<Client>>(jsonData)
                            ?? new List<Client>();
            var client = new Client()
            {
                LastName = lastName, Name = name, SecondLastName = secondLastName, BDay = bDay, NumberDriversLicence = numberDriverLicense
            };
            manager.AssociateManager(client);
            myClients.Add(client);
            File.WriteAllText(@"C:\rentcar\clients.json", JsonConvert.SerializeObject(myClients, Formatting.Indented, settings));

        }
        public void RemoveClient(string numberDriverLicense)
        {
            if (File.Exists(@"C:\rentcar\clients.json"))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };
                var myClients = JsonConvert.DeserializeObject<List<Client>>(File.ReadAllText(@"C:\rentcar\clients.json"), settings)
                                ?? new List<Client>();
                var deleteClient = myClients.Find(client => client.NumberDriversLicence == numberDriverLicense);
                myClients.Remove(deleteClient);
                File.WriteAllText(@"C:\rentcar\clients.json", JsonConvert.SerializeObject(myClients, Formatting.Indented, settings));
            }
            else
            {
                Console.WriteLine("Не существует клиентов! Сначала добавьте");
            }
        }
        public  List<Client> GetClient()
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            var myClients = JsonConvert.DeserializeObject<List<Client>>(File.ReadAllText(@"C:\rentcar\clients.json"), settings)
                            ?? new List<Client>();
            return myClients;
        }
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
