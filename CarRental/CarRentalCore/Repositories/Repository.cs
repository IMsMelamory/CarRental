using System;
using System.Collections.Generic;
using System.Linq;
using CarRentalCore.Model;
using CarRentalCore.Providers;

namespace CarRentalCore.Repositories
{
    public class Repository<T> where T : BaseEntity
    {
        public event Action OnRepositoryUpdate;
        private readonly BaseDataProvider<T> _jsonProvider;
        protected List<T> _entities;

        public Repository(BaseDataProvider<T> jsonProvider)
        {
            _jsonProvider = jsonProvider;
        }

        public List<T> GetAll()
        {
            UpdateDataIfNotExist();
            return _entities;
        }

        public void Add(T entity)
        {
            UpdateDataIfNotExist();
            _entities.Add(entity);
            ForceUpdate();
        }

        public void Remove(T entity)
        {
            UpdateDataIfNotExist();
            _entities.Remove(entity);
            ForceUpdate();
        }

        public void RemoveById(int id)
        {
            UpdateDataIfNotExist();
            var entityToRemove = _entities.FirstOrDefault(x => x.ID == id);
            _entities.Remove(entityToRemove);
            ForceUpdate();
        }
        public void EditById(int id, T entity)
        {
            UpdateDataIfNotExist();
            var oldEntity = _entities.FirstOrDefault(x => x.ID == id);
            _entities.Remove(oldEntity);
            _entities.Add(entity);
            ForceUpdate();
        }

        public void ForceUpdate()
        {
            _jsonProvider.WriteAll(_entities);
            OnOnRepositoryUpdate();
        }

        protected void UpdateDataIfNotExist()
        {
            _entities ??= _jsonProvider.GetAll();
        }
        protected virtual void OnOnRepositoryUpdate()
        {
            OnRepositoryUpdate?.Invoke();
        }
    }
}