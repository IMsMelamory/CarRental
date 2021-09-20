using System;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualBasic;



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
