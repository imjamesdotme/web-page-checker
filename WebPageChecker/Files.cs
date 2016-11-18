using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;

namespace WebPageChecker
{
    class Files
    {
        public static int getNumberOfFiles(HtmlDocument html)
        {
            var xpath = "//*[script]";

            int count = 0;

            if (html.DocumentNode.SelectNodes(xpath) == null)
            {
                count = 0;
            }
            else
            {
                foreach (HtmlNode node in html.DocumentNode.SelectNodes(xpath))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
