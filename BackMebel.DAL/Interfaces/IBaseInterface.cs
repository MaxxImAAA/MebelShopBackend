using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.DAL.Interfaces
{
    public interface IBaseInterface<T>
    {
        Task<bool> Create(T model);

        Task<List<T>> GetAll();

        Task<bool> Delete(T model);

        Task<T> Get(int id);
    }
}
