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
    class Program
    {
        static void Main(string[] args)
        {

            Results Results = new Results();

            Console.WriteLine("Starting..");
            Console.WriteLine("Please input your desired URL..");
            Results.url = Console.ReadLine();

            if (!Results.url.StartsWith("http://"))
            {
                Results.url = "http://" + Results.url;
            }

            Console.WriteLine("Do you want all links on this page? (y/n)");
            string allLinks = Console.ReadLine();

            Console.WriteLine("Do you want to check the link status?");
            string linkStatus = Console.ReadLine();

            Console.WriteLine("Do you want all headings text? (y/n)");
            string allHeadings = Console.ReadLine();

            Console.WriteLine("Do you want all image information? (y/n)");
            string allImages = Console.ReadLine();

            // Request document.
            var html = new HtmlDocument();

            try
            {
                html.LoadHtml(new WebClient().DownloadString(Results.url));
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: {0}", e);
            }

            // Returns all URL's.
            if (allLinks == "y")
            {
                Results.links = Links.getAllLinks(html);
            }

            // Check link status.
            List<string> linkStats = new List<string>();
            if (linkStatus == "y")
            {
                Results.linkStats = Links.getLinkStatus(html, Results.url);
            }

            // Gets all headings on the page.
            List<string> headings = new List<string>();

            if (allHeadings == "y")
            {
                Results.headings = Headings.pageHeadings(html);
            }

            // Returns all image information.
            List<string> allImageDetails = new List<string>();

            if (allImages == "y")
            {
                Results.images = Images.getImageDetails(html);
            }

            // Returns number of scripts.
            Results.scriptFiles = Files.getNumberOfFiles(html);

            // Generate .txt file report.
            Report report = new Report();
            report.createTextFile(Results);
        }

    } // End class.
}
