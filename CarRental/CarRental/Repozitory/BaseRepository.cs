using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace CarRental.Repozitory
{
    public class BaseRepository
    {
        public static List <T> GetAll<T>(string filepath)
        {

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            var jsonData = "";
            if (File.Exists(filepath))
            {
                jsonData = File.ReadAllText(filepath);
            }
            else
            {
                var fileCar = File.Create(filepath);
                fileCar.Close();
            }
            var myList = JsonConvert.DeserializeObject<List<T>>(jsonData)
                         ?? new List<T>();
            return myList;

        }
        public static void WriteAll<T>(string filepath, List<T> list)
        {

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            File.WriteAllText(filepath, JsonConvert.SerializeObject(list, Formatting.Indented, settings));

        }
    }
}
