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
        public void ChekIsFine(string clientDriverLicense, string carNumber, DateTime endRent)
        {
            UpdateDataIfNotExist();
            var findCar = _entities.FirstOrDefault(x => (x.ClientNumberLicense == clientDriverLicense) && (x.CarNumber == carNumber) && (x.EndRent == endRent));
            var dayCount = (findCar.EndRent - findCar.StartRent).Days / 30+1;
            {
                if (dayCount > findCar.DayRentCount)
                {
                    findCar.Fine = (dayCount - findCar.DayRentCount) * 5;
                }
            }
            ForceUpdate();
        }
    }
}
