using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Test
{
    internal class DigimonPostModel
    {

        public class Rootobject
        {
            public Class1[] Property1 { get; set; }
        }

        public class Class1
        {
            public string name { get; set; }
            public string img { get; set; }
            public string level { get; set; }
        }

    }
}
