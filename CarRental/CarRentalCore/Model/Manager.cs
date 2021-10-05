using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CarRentalCore.Model
{
    public class Manager: User
    {
        [JsonIgnore]
        public List<Client> Clients { get; set; } = new List<Client>();
    }

  
}
