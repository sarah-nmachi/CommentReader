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

        public async Task<YoutubeResponse> GetComments(string videoId)
        {
            var client = _httpClientFactory.CreateClient("youtube");
            var response = await client.GetAsync($"commentThreads?part=id%2C%20replies%2C%20snippet&key={_configuration["Youtube-Key"]}&videoId={videoId}");
            
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<YoutubeResponse>(responseString);
                return obj;
            }
            return null;
        }
    }
   
}


