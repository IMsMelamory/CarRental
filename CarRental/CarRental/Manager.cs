using System.Collections.Generic;
using Newtonsoft.Json;


namespace CarRental
{
    public class Manager: User
    {
        [JsonIgnore]
        public List<Client> Clients { get; set; } = new List<Client>();
    }

  
}
