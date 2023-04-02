using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repositories.Computadoras;

namespace Infrastructure.Repositories.Generic
{
    // This is an Unit Of Work Implementation.
    // Ef core already implements Unit Of Work
    // but we're making this, so our code is
    // better to read and maintain.
    // See: https://t.ly/mf9W, https://t.ly/-bTA
    // And: https://t.ly/NC8S, https://t.ly/HXnk
    public interface IRepository
    {
        GenericRepository<T> Generic<T>() where T : class;

        ComputadoraRepository Computadoras { get; }
    }
}
