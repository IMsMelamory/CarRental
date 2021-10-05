using System;
using System.Linq;
using CarRentalCore.Model;
using CarRentalCore.Providers;

namespace CarRentalCore.Repositories
{
    public class RentsRepository : Repository<Rent>
    {
        public RentsRepository(BaseDataProvider<Rent> jsonProvider) : base(jsonProvider)
        {
        }
        public int FindMaxIDRent()
        {
            UpdateDataIfNotExist();
            var id = _entities.Select(x => x.ID);
            if (id.Any())
            {
                return id.Max();
            }
            else
            {
                return 0;
            }
        }

        public Rent FindRentCar(string clientDriverLicense, string carNumber)
        {
            UpdateDataIfNotExist();
            return _entities.FirstOrDefault(x => (x.ClientNumberLicense == clientDriverLicense) && (x.CarNumber == carNumber) && (x.EndRent == DateTime.MinValue));
        }
        public bool ChekDateEndRent(string clientDriverLicense, string carNumber, DateTime endRent)
        {
            UpdateDataIfNotExist();
            if (this.FindRentCar(clientDriverLicense, carNumber).StartRent > endRent)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void UpdateDateEnd(string clientDriverLicense, string carNumber, DateTime endRent)
        {
            UpdateDataIfNotExist();
            this.FindRentCar(clientDriverLicense, carNumber).EndRent = endRent;
            ForceUpdate();
        }
        public void ChekIsFine(string clientDriverLicense, string carNumber)
        {
            UpdateDataIfNotExist();
            var findCar = this.FindRentCar(clientDriverLicense, carNumber);
            {
                if ((findCar.EndRent - findCar.StartRent).TotalDays > findCar.DayRentCount)
                {
                    findCar.Fine = (((findCar.EndRent - findCar.StartRent).TotalDays) - findCar.DayRentCount) * 5;
                }
            }
            ForceUpdate();
        }
    }
}
