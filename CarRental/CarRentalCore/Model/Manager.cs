using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CarRentalCore.Model
{
    public class Manager: User
    {
        [JsonIgnore]
        public List<Client> Clients { get; set; } = new List<Client>();
        public DateTime StartWork { get; set; }
        public DateTime EndWork { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }


    }

  
}
