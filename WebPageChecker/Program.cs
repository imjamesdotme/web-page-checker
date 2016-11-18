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
            Console.WriteLine("Starting..");
            Console.WriteLine("Please input your desired URL..");
            string url = Console.ReadLine();

            if (!url.StartsWith("http://"))
            {
                url = "http://" + url;
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
                html.LoadHtml(new WebClient().DownloadString(url));
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: {0}", e);
            }

            // Returns all URL's.
            List<string> links = new List<string>();

            if (allLinks == "y")
            {
                links = Links.getAllLinks(html);
            }

            // Check link status.
            List<string> linkStats = new List<string>();
            if (linkStatus == "y")
            {
                linkStats = Links.getLinkStatus(html, url);
            }

            // Gets all headings on the page.
            List<string> headings = new List<string>();

            if (allHeadings == "y")
            {
                headings = Headings.pageHeadings(html);
            }

            // Returns all image information.
            List<string> allImageDetails = new List<string>();

            if (allImages == "y")
            {
                allImageDetails = Images.getImageDetails(html);
            }

            // Returns number of scripts.
            int numberOfFiles = Files.getNumberOfFiles(html);
       
            // Write data to a .txt file.
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\results.txt"))
            {
                DateTime localDate = DateTime.Now;

                outputFile.WriteLine("Your report for " + url + " on " + localDate);
                outputFile.WriteLine("**********");
                outputFile.WriteLine("Links on this page:");

                // Page links.
                int urlCount = 0;

                for (var i = 0; i < links.Count; i++)
                {
                    outputFile.WriteLine(links[i]);
                    urlCount++;
                }

                outputFile.WriteLine("Number of URL's: " + urlCount);

                // Link status.
                outputFile.WriteLine("**********");
                outputFile.WriteLine("Link status:");
                for (var i = 0; i < linkStats.Count; i++)
                {
                    outputFile.WriteLine(linkStats[i]);
                }

                // Page headings.
                outputFile.WriteLine("**********");
                outputFile.WriteLine("All page headings:");

                for (var i = 0; i < headings.Count; i++)
                {
                    outputFile.WriteLine(headings[i]);
                }

                // Image details.
                outputFile.WriteLine("**********");
                outputFile.WriteLine("All image details:");

                for (var i = 0; i < allImageDetails.Count; i++)
                {
                    outputFile.WriteLine(allImageDetails[i]);
                }

                // Other.
                outputFile.WriteLine("**********");
                outputFile.WriteLine("Number of script files:" + numberOfFiles);

                // End file writting.
                Console.WriteLine("results.txt outputted to the My Documents folder.");
            }
        }

    } // End class.
}
