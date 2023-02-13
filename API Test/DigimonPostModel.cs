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
            public Digimon[] DigimonProperty { get; set; }
        }

        public class Digimon
        {
            public string name { get; set; }
            public string img { get; set; }
            public string level { get; set; }
        }

    }
}
