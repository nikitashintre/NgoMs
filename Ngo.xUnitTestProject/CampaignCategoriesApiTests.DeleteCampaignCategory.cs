using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Ngo.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ngo.xUnitTestProject
{
    public partial class CampaignCategoriesApiTests
    {
        [Fact]
        public void DeleteCategory_NotFoundResult()
        {
            // ARRANGE
            var dbName = nameof(CampaignCategoriesApiTests.DeleteCategory_NotFoundResult);
            var logger = Mock.Of<ILogger<CampaignCategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new CampaignCategoriesController(dbContext, logger);
            int findCategoryID = 900;

            // ACT
            IActionResult actionResultDelete = apiController.DeleteCampaignCategory(findCategoryID).Result;

            // ASSERT - check if the IActionResult is NotFound 
            Assert.IsType<NotFoundResult>(actionResultDelete);

            // ASSERT - check if the Status Code is (HTTP 404) "NotFound"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound;
            var actualStatusCode = (actionResultDelete as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteCategory_BadRequestResult()
        {
            // ARRANGE
            var dbName = nameof(CampaignCategoriesApiTests.DeleteCategory_BadRequestResult);
            var logger = Mock.Of<ILogger<CampaignCategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new CampaignCategoriesController(dbContext, logger);
            int? findCategoryID = null;

            // ACT
            IActionResult actionResultDelete = apiController.DeleteCampaignCategory(findCategoryID).Result;

            // ASSERT - check if the IActionResult is BadRequest 
            Assert.IsType<BadRequestResult>(actionResultDelete);

            // ASSERT - check if the Status Code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultDelete as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteCategory_OkResult()
        {
            // ARRANGE
            var dbName = nameof(CampaignCategoriesApiTests.DeleteCategory_BadRequestResult);
            var logger = Mock.Of<ILogger<CampaignCategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new CampaignCategoriesController(dbContext, logger);
            int findCategoryID = 1;

            // ACT
            IActionResult actionResultDelete = apiController.DeleteCampaignCategory(findCategoryID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultDelete);

            // ASSERT - if Status Code is HTTP 200 (Ok)
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultDelete as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }
    }
}
