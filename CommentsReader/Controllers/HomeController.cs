using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommentsReader.Models;
using CommentsReader.Interfaces;

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
                var equalsIndex = url.IndexOf("=") + 1;
                var andIndex = url.IndexOf("&");
                var videoId = url.Substring(equalsIndex, andIndex - equalsIndex);

                var youtuberesponse = await _youtubeAPIService.GetComments(videoId);
                if (youtuberesponse == null)
                {
                    TempData["message"] = "An Error Occured ";
                    return RedirectToAction("Index");
                }
                   

                List<CommentModel> comments = new List<CommentModel>();

                foreach (var item in youtuberesponse.items)
                {
                    comments.Add(new CommentModel
                    {
                        CommentString = item.snippet.topLevelComment.snippet.textOriginal,
                        Date = item.snippet.topLevelComment.snippet.publishedAt.ToString(),
                        UserName = item.snippet.topLevelComment.snippet.authorDisplayName,
                        Rating = item.snippet.topLevelComment.snippet.likeCount.ToString()
                    });
                }

               // string csv = String.Join(",", comments.Select(x => x.ToString()).ToArray());
                return null;
            }
            catch (System.Exception)
            {

                
                TempData["message"] = "An Error Occured ";
                return RedirectToAction("Index");
            }


        }
       
    }
}
