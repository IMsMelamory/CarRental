using System;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace CarRental.Repozitory
{
    public class ClientRepository: BaseRepository
    {
        public static void AddClient(string lastName, string name, string secondLastName, DateTime bDay, string numberDriverLicense, Manager manager)
        {
            var filepath = "clients.json";
            var myClients = GetAll<Client>(filepath);
            var client = new Client()
            {
                LastName = lastName, Name = name, SecondLastName = secondLastName, BDay = bDay, NumberDriversLicence = numberDriverLicense
            };
            manager.AssociateManager(client);
            myClients.Add(client);
            WriteAll<Client>(filepath, myClients);
        }
        public static void RemoveClient(string numberDriverLicense)
        {
            if (File.Exists(@"C:\rentcar\clients.json"))
            {
                var filepath = "clients.json";
                var myClients = GetAll<Client>(filepath);
                var deleteClient = myClients.Find(client => client.NumberDriversLicence == numberDriverLicense);
                myClients.Remove(deleteClient);
                WriteAll<Client>(filepath, myClients);
            }
            else
            {
                Console.WriteLine("Не существует клиентов! Сначала добавьте");
            }
        }
        public static List<Client> GetClients()
        {
            var filepath = "clients.json";
            var myClients = GetAll<Client>(filepath);
            return myClients;
        }
    }
}
