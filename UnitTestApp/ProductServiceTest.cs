using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using TestTaskPostolenko.Controllers;
using Services.Services;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTestApp.Tests
{
	public class ProductServiceTest
	{
		public ILogger<ProductService> GetLogger()
        {
			return Mock.Of<ILogger<ProductService>>();
		}

		[Fact]
		public async Task Product_Service_Test_GetAllProductsReturns48Values()
		{
			var logger = GetLogger();
			// Arrange
			var service = new ProductService(logger);
			// Act
			var products = await service.GetAllProductsFromStorage();

			// Assert
			Assert.Equal(48, products.Count());
		}

		[Fact]
		public async Task Product_Service_Test_GetFilteredObjectWorksCorrectlyWithAllParameters()
		{
			// Arrange
			var logger = GetLogger();

			var service = new ProductService(logger);
			// Act
			var products = await service.GetFilteredOrders(15, 10, new string[] { "medium", "small" }, new string[] { "hat", "belt" });

			// Assert
			Assert.Equal(6, products.Count());
		}

		[Fact]
		public async Task Product_Service_Test_GetFilteredObjectWorksCorrectlyWithout()
		{
			// Arrange
			var logger = GetLogger();

			var service = new ProductService(logger);
			// Act
			var products = await service.GetFilteredOrders();

			// Assert
			Assert.Equal(48, products.Count());
		}

		[Fact]
		public async Task Product_Service_Test_NegativePriceWillGive0()
		{
			// Arrange
			var logger = GetLogger();

			var service = new ProductService(logger);
			// Act
			var products = await service.GetFilteredOrders(-15);

			// Assert
			Assert.Empty(products);
		}

		[Fact]
		public async Task Product_Service_Test_GetFilteredOrdersWillNotReturnEmptyResultWithNoParameters()
		{
			// Arrange
			var logger = GetLogger();

			var service = new ProductService(logger);
			// Act
			var products = await service.GetFilteredOrders();

			// Assert
			Assert.NotEmpty(products);
		}

		[Fact]
		public async Task Product_Service_Test_GetFilteredOrdersWillNotReturnNull()
		{
			// Arrange
			var logger = GetLogger();

			var service = new ProductService(logger);
			// Act
			var products = await service.GetFilteredOrders();

			// Assert
			Assert.NotNull(products);
		}

		[Fact]
		public async Task Product_Service_Test_MostCommonWordsDoNotHaveEmptyStrings()
		{
			// Arrange
			var logger = GetLogger();

			var service = new ProductService(logger);
			// Act

			var allProducts = await service.GetAllProductsFromStorage();
            var mostCommonWords = service.Get10MostCommonWordsExcept5MostCommon(allProducts.Select(x => x.Description));

            // Assert
            Assert.All(mostCommonWords, x => Assert.NotEqual("", x));
        }

		[Fact]
		public async Task Product_Service_Test_MostCommonWordsWasTrimmed()
		{
			// Arrange
			var logger = GetLogger();

			var service = new ProductService(logger);
			// Act

			var allProducts = await service.GetAllProductsFromStorage();
            var mostCommonWords = service.Get10MostCommonWordsExcept5MostCommon(allProducts.Select(x => x.Description));

            // Assert
            Assert.All(mostCommonWords, x => Assert.True(x[0] != ' ' && x[x.Length - 1] != ' '));
        }

		[Fact]
		public async Task Test_Get10MostCommonWordsExcept5MostCommon_Method()
		{
			var logger = GetLogger();
			// Arrange 
			var service = new ProductService(logger);
			// Act 

			var products = await service.GetAllProductsFromStorage();
			var allDescriptions = products.Select(x => x.Description);

			var result = service.Get10MostCommonWordsExcept5MostCommon(allDescriptions);

			// Assert 
			Assert.DoesNotContain(result, word => word.Length < 2 && string.IsNullOrWhiteSpace(word));
		}

		[Fact]
		public async Task Product_Service_Test_FilterNullWillNotGiveAnyProblem()
		{
			// Arrange
			var logger = GetLogger();

			var service = new ProductService(logger);
			// Act
			var products = await service.GetFilteredOrders(null, null, null, null);

			// Assert
			Assert.NotEmpty(products);
			Assert.NotNull(products);
		}
	}
}
