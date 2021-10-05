namespace CarRentalCore.Model
{
    public class Car : BaseEntity
    {
        public string Number { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string DateRelease { get; set; }
        public int DayPrice { get; set; }
    }
}
