using System.Collections.Generic;
using CarRentalCore.Model;

namespace CarRentalCore.Providers
{
    public class MssqlProvider<T> : BaseDataProvider<T> where T : BaseEntity
    {
        private readonly string _connectionString;

        public MssqlProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public override List<T> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public override void WriteAll(List<T> list)
        {
            throw new System.NotImplementedException();
        }
    }
}