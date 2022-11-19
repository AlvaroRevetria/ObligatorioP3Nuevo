using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorio <T>
    {
        void Add(T obj);
        void Remove(int Id);
        void Update(T obj);
        T FindById(int Id);
        IEnumerable<T> FindAll();

    }
}
