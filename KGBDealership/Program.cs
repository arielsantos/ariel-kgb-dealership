using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using KgbDealershipBusiness.Entities;
using KgbDealershipBusiness.Services;

namespace KGBDealership
{
    class Program
    {

        static async System.Threading.Tasks.Task Main(string[] args)
        {

            List<Review> listReview = await new ReviewService().GetTopReviews();

            if (listReview.Count <= 0) 
                Console.WriteLine("There was a problem reaching the website. Try again later");
            else { 
                Console.WriteLine(@"WELCOME COMRADE! ");
                Console.WriteLine(@"After our analysis the following reviews looks suspicious: ");

                listReview.ForEach(delegate (Review _review) {
                    Console.WriteLine("Date: " + _review.Date + " Title: " + _review.Title + " Rating: " + _review.Rating);

                });


            }            
        }
    }
}

