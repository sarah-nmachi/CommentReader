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

        public Task<YoutubeResponse> GetComments(string videoId)
        {
            throw new NotImplementedException();
        }
    }
}
