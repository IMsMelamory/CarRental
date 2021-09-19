using System.Collections.Generic;
using System.Linq;
using System;
using CarRental.Providers;

namespace CarRental.Repositories
{
    public class RentsRepository : Repository<Rent>
    {
        public RentsRepository(BaseDataProvider<Rent> jsonProvider) : base(jsonProvider)
        {
        }
        public List<Rent> FindRentCar(string clientDriverLicense, string carNumber)
        {
            UpdateDataIfNotExist();
            return _entities.Where(x => (x.ClientNumberLicense == clientDriverLicense) && (x.CarNumber == carNumber)).ToList();
        }
        public bool ChekDateEndRent(string clientDriverLicense, string carNumber, DateTime endRent)
        {
            UpdateDataIfNotExist();
            var count = 0;
            foreach (var rentcar in this.FindRentCar(clientDriverLicense, carNumber))
            {
                if (rentcar.StartRent > endRent)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void UpdateDateEnd(string clientDriverLicense, string carNumber, DateTime endRent)
        {
            UpdateDataIfNotExist();
            foreach (var findCar in this.FindRentCar(clientDriverLicense, carNumber))
            {
                findCar.EndRent = endRent;
            }
            ForceUpdate();
        }
        public void ChekIsFine(string clientDriverLicense, string carNumber)
        {
            UpdateDataIfNotExist();
            foreach (var findCar in this.FindRentCar(clientDriverLicense, carNumber))
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
