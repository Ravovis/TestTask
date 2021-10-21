using Infrastrucuture;
using Infrastrucuture.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System.Threading.Tasks;

namespace TestTaskPostolenko.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        private IProductService _productService { get; set; }
        private ILogger<ProductController> _logger { get; set; }
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
