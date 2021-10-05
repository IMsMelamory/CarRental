using System;

namespace CarRentalCore.Model
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime BDay { get; set; }
       
    }
}
