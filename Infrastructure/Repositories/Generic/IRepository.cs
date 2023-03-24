using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Generic
{
    //This is an Unit Of Work Implementation
    //See: https://t.ly/mf9W, https://t.ly/-bTA
    //And: https://t.ly/NC8S
    public interface IRepository : IDisposable
    {

        GenericRepository<T> Generic<T>() where T : class;

        ComputadorasRepository Computadoras { get; }

        int SaveChanges();
    }
}
