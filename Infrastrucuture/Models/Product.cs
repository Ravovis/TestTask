using System.Collections.Generic;

namespace Infrastrucuture.Models
{
    public class Product
    {
        public string Title { get; set; }
        public long Price { get; set; }
        public IEnumerable<string> Sizes { get; set; }
        public string Description { get; set; }
    }

    public class ProductCollection
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
