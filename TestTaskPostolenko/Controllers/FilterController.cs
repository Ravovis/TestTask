using Infrastrucuture;
using Infrastrucuture.DTO;
using Infrastrucuture.Models;
using Infrastrucuture.Result_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTaskPostolenko.Controllers
{
    /// <summary>
    /// Product Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FilterController : ControllerBase
    {
        /// <summary>
        /// Product Controller Constructor
        /// </summary>
        public FilterController(IProductService productService, ILogger<FilterController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        private IProductService _productService { get; set; }
        private ILogger<FilterController> _logger { get; set; }

        /// <summary>
        /// Gets all Product with filters
        /// </summary>
        /// <param name="productSearchDTO">Product Search DTO</param>
        /// <returns>List of Products</returns>
        /// <response code="200">Success</response>
        /// <response code="500">Unexpected Error</response>
        /// <example>
        /// GET: /product
        /// </example>
        public async Task<APIResult<IEnumerable<Product>>> Get([FromQuery] ProductSearchDTO productSearchDTO)
        {
            if (ModelState.IsValid)
            {
                var products = await _productService.GetFilteredOrders(productSearchDTO.Maxprice, productSearchDTO.Minprice, productSearchDTO?.Sizes?.Split(","), productSearchDTO?.Highlight?.Split(","));

                return new APIResult<IEnumerable<Product>> { Data = products, StatusCode = HttpContext.Response.StatusCode };
            }
            else
            {
                _logger.LogError(LogMessages.ModelWasInvalid);
                return new APIResult<IEnumerable<Product>> { ErrorMessage = LogMessages.ModelWasInvalid, StatusCode = HttpContext.Response.StatusCode };
            }
        }
    }
}
