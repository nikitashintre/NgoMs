using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace Ngo.xUnitTestProject
{
    public partial class CampaignCategoriesApiTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public CampaignCategoriesApiTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}
