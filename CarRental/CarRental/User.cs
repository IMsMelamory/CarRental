using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarRental.Repozitory.BaseRepository;

namespace CarRental
{
    public class User
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime BDay { get; set; }
       
    }
}
