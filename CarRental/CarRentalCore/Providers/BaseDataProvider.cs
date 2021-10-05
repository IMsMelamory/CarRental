using System.Collections.Generic;
using CarRentalCore.Model;

namespace CarRentalCore.Providers
{
    public abstract class BaseDataProvider<T> where T : BaseEntity
    {
        public abstract List<T> GetAll();
        public abstract void WriteAll(List<T> list);
    }
}