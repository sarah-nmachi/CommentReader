using CommentsReader.Interfaces;
using CommentsReader.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommentsReader.Services
{
    public class YoutubeV3Service : IYoutubeAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public YoutubeV3Service(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // RETURNS THE WHOLE COMMENT UP TO 1000 BECAUSE OF TIME OUT
        public async Task<List<Item>> GetComments(string videoId)
        {
            List<Item> items = new List<Item>();

            var client = _httpClientFactory.CreateClient("youtube");

            var pageToken = "";
            while(true)
            {
                var response = await client.GetAsync($"commentThreads?part=id%2C%20replies%2C%20snippet&pageToken={pageToken}&maxResults=100&moderationStatus=published&videoId={videoId}&key={_configuration["Youtube-Key"]}");
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                   

                    var obj = JsonConvert.DeserializeObject<YoutubeResponse>(responseString);
                    
                    pageToken = obj.nextPageToken;

                    List<Item> currentItems = obj.items;
                    items.AddRange(currentItems);

                    //The server times out if it waits for too long.
                    //So let's get the first 1000 if the comments are more than 1000
                    if (obj.nextPageToken == null || items.Count >= 1000)
                    {
                        break;
                    }
                }
            }

            return items;
        }

        /// tHIS LINE OF CODE ONLY RETURNS THE FIRST PAGE OF YOU TUBE COMMENTS
        //public async Task<YoutubeResponse> GetComments(string videoId)
        //{
        //    var client = _httpClientFactory.CreateClient("youtube");
            
        //    var response = await client.GetAsync($"commentThreads?part=id%2C%20replies%2C%20snippet&maxResults=100&moderationStatus=published&videoId={videoId}&key={_configuration["Youtube-Key"]}");
            
          
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseString = await response.Content.ReadAsStringAsync();
        //          var  obj = JsonConvert.DeserializeObject<YoutubeResponse>(responseString);
        //        var nextPageToken = obj.nextPageToken;
                
        //        return obj;
        //        }
           
           
        //    return null;
        //}
    }
   
}


