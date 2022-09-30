using Castle.Components.DictionaryAdapter.Xml;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Ngo.Controllers;
using Ngo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ngo.xUnitTestProject
{
    public partial class CampaignCategoriesApiTests
    {
        [Fact]
        public void InsertCategory_OkResult()
        {
            // ARRANGE
            var dbName = nameof(CampaignCategoriesApiTests.InsertCategory_OkResult);
            var logger = Mock.Of<ILogger<CampaignCategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new CampaignCategoriesController(dbContext, logger);
            CampaignCategory categoryToAdd = new CampaignCategory
            {
                CategoryId = 5,
                CategoryName = "New Category"             // IF = null, then: INVALID!  CategoryName is REQUIRED
            };

            // ACT
            IActionResult actionResultPost = apiController.PostCampaignCategory(categoryToAdd).Result;

            // ASSERT - check if the IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultPost);

            // ASSERT - check if the Status Code is (HTTP 200) "Ok", (HTTP 201 "Created")
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultPost as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);

            // Extract the result from the IActionResult object.
            var postResult = actionResultPost.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT - if the result is a CreatedAtActionResult
            Assert.IsType<CreatedAtActionResult>(postResult.Value);

            // Extract the inserted Category object
            CampaignCategory actualCategory = (postResult.Value as CreatedAtActionResult).Value
                                      .Should().BeAssignableTo<CampaignCategory>().Subject;

            // ASSERT - if the inserted Category object is NOT NULL
            Assert.NotNull(actualCategory);

            Assert.Equal(categoryToAdd.CategoryId, actualCategory.CategoryId);
            Assert.Equal(categoryToAdd.CategoryName, actualCategory.CategoryName);
        }
        
        [Fact]
        public void InsertCategory_BadRequestResult()
        {
            // ARRANGE
            var dbName = nameof(CampaignCategoriesApiTests.InsertCategory_BadRequestResult);
            var logger = Mock.Of<ILogger<CampaignCategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new CampaignCategoriesController(dbContext, logger);
            CampaignCategory categoryToAdd = new CampaignCategory
            {
                CategoryId = 2,
                CategoryName = "New Category"             // IF = null, then: INVALID!  CategoryName is REQUIRED
            };

            // ACT
            IActionResult actionResultPost = controller.PostCampaignCategory(categoryToAdd).Result;

            // ASSERT - check if the IActionResult is BadRequestResult
            Assert.IsType<BadRequestObjectResult>(actionResultPost);

            // ASSERT - check if the Status Code is Bad
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultPost as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);

            // Extract the result from the IActionResult object.
            var postResult = actionResultPost.Should().BeOfType<BadRequestResult>().Subject;

            //// ASSERT - if the result is a BadActionResult
            Assert.IsType<BadRequestResult>(postResult);

            //// Extract the inserted Category object
            CampaignCategory actualCategory = (postResult as BadRequestResult)
                                      .Should().BeAssignableTo<CampaignCategory>().Subject;

            // ASSERT - if the inserted Category object is NOT NULL
            Assert.NotNull(actualCategory);

            Assert.Equal(categoryToAdd.CategoryId, actualCategory.CategoryId);
            Assert.Equal(categoryToAdd.CategoryName, actualCategory.CategoryName);

        }

    }
}
