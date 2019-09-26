using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommentsReader.Models;
using CommentsReader.Interfaces;
using ServiceStack.Text;

namespace CommentsReader.Controllers
{
    public class HomeController : Controller
    {
        private readonly IYoutubeAPIService _youtubeAPIService;
        public HomeController(IYoutubeAPIService youtubeAPIService)
        {
            _youtubeAPIService = youtubeAPIService;
        }
        public  IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Comments(string url)
        {
            try
            {
                // sample url https://www.youtube.com/watch?v=bATXe1XhIZ8
                var equalsIndex = url.IndexOf("=") + 1;
                
                var videoId = url.Substring(equalsIndex, url.Length - equalsIndex);
                

                var youtuberesponse = await _youtubeAPIService.GetComments(videoId);
                if (youtuberesponse == null)
                {
                    TempData["message"] = "An Error Occured ";
                    return RedirectToAction("Index");
                }
                   

                List<CommentModel> comments = new List<CommentModel>();

                foreach (var item in youtuberesponse)
                {
                    comments.Add(new CommentModel
                    {
                        CommentString = item.snippet.topLevelComment.snippet.textOriginal,
                        Date = item.snippet.topLevelComment.snippet.publishedAt.ToString(),
                        UserName = item.snippet.topLevelComment.snippet.authorDisplayName,
                        Rating = item.snippet.topLevelComment.snippet.likeCount.ToString()
                    });
                }


                // convert to csv
                string csv = CsvSerializer.SerializeToCsv(comments);
                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "YoutubeComment.csv");
               
            }
            catch (System.Exception)
            {


                TempData["message"] = "An Error Occured ";
                return RedirectToAction("Index");
            }


        }
       
    }
}
