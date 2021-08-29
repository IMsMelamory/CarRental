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
        public void AddManager(string lastName, string name, string secondLastName, DateTime bDay)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            var jsonData = "";
            if (File.Exists(@"C:\rentcar\managers.json"))
            {
                jsonData = File.ReadAllText(@"C:\rentcar\managers.json");
            }
            else
            {
                Directory.CreateDirectory(@"C:\rentcar");
                var FileCar = File.Create(@"C:\rentcar\managers.json");
                FileCar.Close();

            }
            var Managers = JsonConvert.DeserializeObject<List<Manager>>(jsonData)
                            ?? new List<Manager>();
            var manager = new Manager()
            {
                LastName = lastName, Name = name, SecondLastName = secondLastName, BDay = bDay,
            };
           Managers.Add(manager);
           File.WriteAllText(@"C:\rentcar\managers.json", JsonConvert.SerializeObject(Managers, Formatting.Indented, settings));

        }
        public void RemoveManager(string lastname)
        {
            if (File.Exists(@"C:\rentcar\managers.json"))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };
                var Managers = JsonConvert.DeserializeObject<List<Manager>>(File.ReadAllText(@"C:\rentcar\managers.json"), settings)
                                ?? new List<Manager>();
                var deleteManager = Managers.Find(manager => manager.LastName == lastname);
                Managers.Remove(deleteManager);
                File.WriteAllText(@"C:\rentcar\managers.json", JsonConvert.SerializeObject(Managers, Formatting.Indented, settings));
            }
            else
            {
                Console.WriteLine("Не существует менеджеров! Сначала добавьте");
            }
        }
        public Manager FindManager(string lastname)
        {
            if (File.Exists(@"C:\rentcar\managers.json"))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };
                var Managers = JsonConvert.DeserializeObject<List<Manager>>(File.ReadAllText(@"C:\rentcar\managers.json"), settings)
                                ?? new List<Manager>();
                var FindManager = Managers.Find(manager => manager.LastName.ToLower() == lastname.ToLower());
                if (FindManager != null)
                {
                    return FindManager;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        public void AssociateManager( Client client)
        {
                client.ClientAssociateManager(this);
        }
    }

  
}
