using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Test
{
    internal class DummyResponseModel
    {
        public class Rootobject
        {
            public Post[] posts { get; set; }
            public int total { get; set; }
            public int skip { get; set; }
            public int limit { get; set; }
        }

        public class Post
        {
            public int id { get; set; }
            public string title { get; set; }
            public string body { get; set; }
            public int userId { get; set; }
            public string[] tags { get; set; }
            public int reactions { get; set; }
        }

    }
}
