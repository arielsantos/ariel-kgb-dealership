using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KgbDealershipBusiness.Entities;

namespace KgbDealershipBusiness.Services
{
    /// <summary>
    /// ReviewService interface
    /// </summary>
    public interface IReviewService
    {


        /// <summary>
        /// Scrapes que website in search for the most well rated reviews
        /// </summary>
        /// <param></param>
        /// <returns>Return the 3 reviews with most positive </returns>
        Task<List<Review>> GetTopReviews();

    }
}
