using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageChecker
{
    class Results
    {
        public string url { get; set; }

        public List<string> links { get; set; }
        public List<string> linkStats { get; set; }
        public List<string> headings { get; set; }
        public List<string> images { get; set; }
        public int scriptFiles { get; set; }
    }
}
