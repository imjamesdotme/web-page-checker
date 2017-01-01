using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.IO;

namespace WebPageChecker
{
    class Links
    {
        public static List<string> getAllLinks(HtmlDocument html)
        {
            List<string> links = new List<string>();

            var linkNodes = html.DocumentNode.SelectNodes("//a[@href]");

            foreach (var linkNode in linkNodes)
            {
                string hrefValue = linkNode.GetAttributeValue("href", string.Empty);
                string titleValue = linkNode.GetAttributeValue("title", string.Empty);

                if (titleValue == null || titleValue == "")
                {
                    titleValue = "NO TITLE TEXT.";
                }

                links.Add(hrefValue + " *** " + titleValue);
            }

            return links;
        }

        public static List<string> getLinkStatus(HtmlDocument html, string url)
        {
            List<string> links = new List<string>();

            var linkNodes = html.DocumentNode.SelectNodes("//a[@href]");

            foreach (var linkNode in linkNodes)
            {

                Console.WriteLine("Checking URL status");
                string hrefValue = linkNode.GetAttributeValue("href", string.Empty);

                // Logic required for PHP files - i.e href="contact.php"

                if (hrefValue.StartsWith("mailto") || hrefValue.StartsWith("tel") || hrefValue.StartsWith("#"))
                {
                    break;
                }
                else if (hrefValue.StartsWith("/"))
                {
                    hrefValue = url + hrefValue;
                }
         
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hrefValue);
                request.Timeout = 10000;
                HttpWebResponse response = null;
                HttpStatusCode statusCode;

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                    //status = (int)response.StatusCode;
                }
                catch (WebException we)
                {
                    //status = 123;
                    response = (HttpWebResponse)we.Response;
                }

                statusCode = response.StatusCode;
                response.Close();
                links.Add(hrefValue + " *** " + statusCode);              

            }

            return links;
        }
    }
}
