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
	}
}
