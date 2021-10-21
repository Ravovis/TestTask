using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastrucuture.DTO
{
    public class ProductSearchDTO
    {
        [Range(0, Int32.MaxValue)]
        public int? Maxprice { get; set; } = null;
        [Range(0, Int32.MaxValue)]
        public int? Minprice { get; set; } = null;
        public string Sizes { get; set; } = null;
        public string Highlight { get; set; } = null;
    }
}