using HtmlAgilityPack;
using KgbDealershipBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;



namespace KgbDealershipBusiness.Services
{

    public class ReviewService
    {

        /// <summary>
        /// Scrapes the website in search for the most well rated reviews
        /// </summary>
        /// <param></param>
        /// <returns>Return the 3 reviews with most positive </returns>
        public async Task<List<Review>> GetTopReviews()
        {
            List<Review> listReview = new List<Review>();

            int pageNumber = 1;

            //Scrape the website reviews from page 1 to 5
            while (pageNumber <= 5)
            {
                listReview.AddRange(await GetReviewsByPage(pageNumber));

                pageNumber++;
            }


            listReview = listReview.OrderByDescending(r => r.Rating).ToList();


            return listReview.Take(3).ToList();

        }

        /// <summary>
        /// Scrapes all the reviews
        /// </summary>
        /// <param></param>
        /// <returns>Return the 3 reviews with most positive </returns>
        private async Task<List<Review>> GetReviewsByPage(int pageNumber)
        {

            List<Review> listReview = new List<Review>();
            var url = "https://www.dealerrater.com/dealer/McKaig-Chevrolet-Buick-A-Dealer-For-The-People-dealer-reviews-23685/page" + pageNumber.ToString();


            HttpClient httpClient = new HttpClient();
            string html = await httpClient.GetStringAsync(url);


            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);


            List<HtmlNode> htmlReview = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("review-entry")).ToList();


            htmlReview.ForEach(delegate (HtmlNode _reviewNode)
            {
                Review review = new Review();

                review.Title = _reviewNode.Descendants("h3").FirstOrDefault().InnerText;


                //get the node with the date
                HtmlNode dateNode = _reviewNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("italic col-xs-6 col-sm-12 pad-none margin-none font-20")).FirstOrDefault();

                review.Date = dateNode.InnerText;

                //get the node with the rating
                HtmlNode ratingNode = _reviewNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("rating-static hidden-xs rating-")).FirstOrDefault();
                string rating = ratingNode.OuterHtml.Replace("<div class=\"rating-static hidden-xs rating-", "").Replace(" margin-center\"></div>", "");


                review.Rating = ParseRating(rating);


                listReview.Add(review);
            });

            return listReview;

        }


        #region .: Helpers :.

        private decimal ParseRating(string rating) 
        {
            decimal decRating = 0;

            Decimal.TryParse(rating,out decRating);

           
            return decRating / 10;
        }

        #endregion


    }
}
