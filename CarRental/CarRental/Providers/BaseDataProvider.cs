using System.Collections.Generic;
using CarRental.Entities;

namespace CarRental.Providers
{
    public abstract class BaseDataProvider<T> where T : BaseEntity
    {
        public abstract List<T> GetAll();
        public abstract void WriteAll(List<T> list);
    }
}