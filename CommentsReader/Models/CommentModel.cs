using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentsReader.Models
{
    public class CommentModel
    {
        public string CommentString { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
        public string Link { get; set; }
        public string Rating { get; set; }

    }
}
