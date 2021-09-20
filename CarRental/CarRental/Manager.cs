using System;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;


namespace CarRental
{
    public class Manager: User
    {
        [JsonIgnore]
        public List<Client> Clients { get; set; } = new List<Client>();
    }

  
}
