using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repozitory
{
    public class ManagerRepository: BaseRepository
    {
        public static void AddManager(string lastName, string name, string secondLastName, DateTime bDay)
        {
            var filepath = "managers.json";
            var managers = GetAll<Manager>(filepath);
            managers.Add(new Manager()
            {
                LastName = lastName,
                Name = name,
                SecondLastName = secondLastName,
                BDay = bDay,
            });
            WriteAll<Manager>(filepath, managers);

        }
        public static void RemoveManager(string lastname)
        {
            if (File.Exists(@"C:\rentcar\managers.json"))
            {
                var filepath = "managers.json";
                var managers = GetAll<Manager>(filepath);
                managers.Remove(managers.Find(manager => manager.LastName == lastname));
                WriteAll<Manager>(filepath, managers);
            }
            else
            {
                Console.WriteLine("Не существует менеджеров! Сначала добавьте");
            }
        }
        public static Manager FindManager(string lastname)
        {
            if (File.Exists(@"C:\rentcar\managers.json"))
            {
                var filepath = "managers.json";
                var managers = GetAll<Manager>(filepath);
                var findManager = managers.Find(manager => manager.LastName.ToLower() == lastname.ToLower());
                if (findManager != null)
                {
                    return findManager;
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
    }
}
