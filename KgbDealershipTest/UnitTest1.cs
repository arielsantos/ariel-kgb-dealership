using KgbDealershipBusiness.Entities;
using KgbDealershipBusiness.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KgbDealershipTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestGetTopReviews()
        {
            List<Review> listReview = await new ReviewService().GetTopReviews();


            Assert.IsTrue(listReview.Count == 3, "Test has failed");
        }
    }
}
