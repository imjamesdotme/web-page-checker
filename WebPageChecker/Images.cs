using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;

namespace WebPageChecker
{
    class Images
    {
        public static List<string> getImageDetails(HtmlDocument html)
        {

            var imageNodes = html.DocumentNode.Descendants("img");

            List<string> images = new List<string>();
            int imageCount = 0;

            foreach (var imageNode in imageNodes)
            {

                string source = "";
                string alt = "";
                imageCount++;

                try
                {   
                    alt = imageNode.Attributes["alt"].Value;

                    if (alt == null || alt == "")
                    {
                        alt = "NO ALT TEXT.";
                    }
                }
                catch
                {
                    alt = "NO ALT ATTRIUTE FOUND!";
                }

                try
                {
                    source = imageNode.Attributes["src"].Value;
                }
                catch
                {
                    source = "NO SRC ATTRIBUTE FOUND!";
                }

                images.Add(source + " *** " + alt);

            }

            images.Add("Total number of images: " + imageCount);
            return images;

        }
    }
}
