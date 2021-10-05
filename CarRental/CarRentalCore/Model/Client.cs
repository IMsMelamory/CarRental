using System.Text.Json.Serialization;

namespace CarRentalCore.Model
{
    public class Client : User
    {
        public string NumberDriversLicence { get; set; }
        public int ManagerID { get; set; }
        
        [JsonIgnore]
        public Manager Manager { get; set; }
    }
}
