using Infrastrucuture.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetFilteredOrders(int? maxprice = null, int? minprice = null, string[] sizes = null, string[] words = null);
    }
}
