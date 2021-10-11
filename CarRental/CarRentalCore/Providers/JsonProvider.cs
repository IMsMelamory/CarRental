using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CarRentalCore.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CarRentalCore.Providers
{
    public class JsonProvider<T> : BaseDataProvider<T> where T : BaseEntity
    {
        private readonly string _jsonPath;
        private readonly JsonSerializerSettings _jsonSettings;

        public JsonProvider(string jsonPath)
        {
            _jsonPath = jsonPath;
            _jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
                /* TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                 PreserveReferencesHandling = PreserveReferencesHandling.Objects,*/
            };
        }

        public override List<T> GetAll()
        {
            var jsonData = "";
            if (File.Exists(_jsonPath))
            {
                jsonData = File.ReadAllText(_jsonPath);
                    
            }
            else
            {
                var fileCar = File.Create(_jsonPath);
                fileCar.Close();
            }

            var myList = JsonConvert.DeserializeObject<List<T>>(jsonData, _jsonSettings) ?? new List<T>();
            return myList;
        }

        public override void WriteAll(List<T> list)
        {
            File.WriteAllText(_jsonPath, JsonConvert.SerializeObject(list, Formatting.Indented, _jsonSettings));
        }
    }
}