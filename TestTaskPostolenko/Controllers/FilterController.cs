using Infrastrucuture;
using Infrastrucuture.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
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
        public async Task<IActionResult> Get([FromQuery] ProductSearchDTO productSearchDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _productService.GetFilteredOrders(productSearchDTO.Maxprice, productSearchDTO.Minprice, productSearchDTO?.Sizes?.Split(","), productSearchDTO?.Highlight?.Split(",")));
            }
            else
            {
                _logger.LogError(LogMessages.ModelWasInvalid);
                return BadRequest();
            }
        }
    }
}
