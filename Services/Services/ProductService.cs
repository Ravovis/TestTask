using Infrastrucuture;
using Infrastrucuture.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductService : IProductService
    {

        public ProductService(ILogger<ProductService> logger)
        {
            _logger = logger;
        }

        private ILogger<ProductService> _logger { get; set; }

        public async Task<IEnumerable<Product>> GetFilteredOrders(int? maxprice = null, int? minprice = null, string[] sizes = null, string[] highlightWords = null)
        {
            var allProducts = await GetAllProductsFromStorage();
            var mostCommonWords = Get10MostCommonWordsExcept5MostCommon(allProducts.Select(x => x.Description));
            var answer = allProducts
                .Where(x => (minprice != null) ? x.Price >= minprice : true)
                .Where(x => (maxprice != null) ? x.Price <= maxprice : true)
                .Where(x => (sizes != null) ? sizes.All(y => x.Sizes.Contains(y)) : true)
                .Where(x => mostCommonWords.Any(y => x.Description.Contains(y)));
            return answer.Select(x => HighlightWords(x, highlightWords));
        }

        public async Task<IEnumerable<Product>> GetAllProductsFromStorage()
        {
            string content;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://www.mocky.io/v2/5e307edf3200005d00858b49");
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(LogMessages.GotFromMocky);

                content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ProductCollection>(content).Products;
                return result;
            }
            else
            {
                _logger.LogInformation(LogMessages.DidntGotFromMocky);
                return new List<Product>();
            }
        }

        public IEnumerable<string> Get10MostCommonWordsExcept5MostCommon(IEnumerable<string> stringsToProceed)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendJoin(" ", stringsToProceed.Select(x=>x.Trim()));

            var result =  Regex.Split(sb.ToString(), @"\W|_")
                .Where(x=>x.Length>=1)
                .GroupBy(x => x)
                .Select(x => new
                {
                    KeyField = x.Key,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .Skip(5).Take(10).Select(x => x.KeyField);

            _logger.LogInformation(LogMessages.GotMostCommonWords);

            return result;
        }


        public Product HighlightWords(Product product, IEnumerable<string> wordsToHighlight)
        {
            if (wordsToHighlight == null) return product;
            StringBuilder sb = new StringBuilder(product.Description);
            foreach (var word in wordsToHighlight)
                sb.Replace(word, $"<em>{word}</em>");
            product.Description = sb.ToString();

            _logger.LogInformation(LogMessages.WordsWereHighlighted);
            return product;
        }
    }
}


