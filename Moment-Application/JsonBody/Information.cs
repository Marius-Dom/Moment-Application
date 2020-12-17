using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moment_Application
{
    public class Information
    {
        public bool initialization { get; set; }
        public string nickname { get; set; }
        public bool darkModeOn { get; set; }
        public Score score { get; set; }

        public class Score
        {
            public int improve { get; set; }
            public int maxScore { get; set; }
        }
    }
}
