using Newtonsoft.Json;

namespace CarRental
{
    public class Client : User
    {
        public string NumberDriversLicence { get; set; }
        public int ManagerID { get; set; }
        
        [JsonIgnore]
        public Manager Manager { get; set; }
    }
}
