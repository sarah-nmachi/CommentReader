﻿using CommentsReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentsReader.Interfaces
{
    public interface IYoutubeAPIService
    {
        //Task<YoutubeResponse> GetComments(string videoId);
        Task<List<Item>> GetComments(string videoId);
    }
}
