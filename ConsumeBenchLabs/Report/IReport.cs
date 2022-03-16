using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsumeBenchLabs
{
    public interface IReport<T>
    {
        Task<IList<T>> GetPagesAsync();
    }
}
