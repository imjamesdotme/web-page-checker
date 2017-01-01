using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPageChecker
{
    class Report
    {

        public void createTextFile(Results Results)
        {
            DateTime localDate = DateTime.Now;
            string formattedDate = DateTime.Now.ToString("ddMMyyyy HHmm");
            string fileName = cleanFileName(Results.url) + "-" + formattedDate + ".txt";

            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                outputFile.WriteLine("Your report for " + Results.url + " on " + localDate);
                outputFile.WriteLine("**********");
                outputFile.WriteLine("Links on this page:");

                // Page links.
                int urlCount = 0;

                foreach (var link in Results.links)
                {
                    outputFile.WriteLine(link);
                    urlCount++;
                }

                outputFile.WriteLine("Number of URL's: " + urlCount);

                // Link status.
                outputFile.WriteLine("**********");
                outputFile.WriteLine("Link status:");
                foreach (var linkStat in Results.linkStats)
                {
                    outputFile.WriteLine(linkStat);
                }

                // Page headings.
                outputFile.WriteLine("**********");
                outputFile.WriteLine("All page headings:");
                foreach (var heading in Results.headings)
                {
                    outputFile.WriteLine(heading);
                }

                // Image details.
                outputFile.WriteLine("**********");
                outputFile.WriteLine("All image details:");
                foreach (var image in Results.images)
                {
                    outputFile.WriteLine(image);
                }

                // Other.
                outputFile.WriteLine("**********");
                outputFile.WriteLine("Number of script files:" + Results.scriptFiles);

                // End file writting.
                Console.WriteLine("Results file for " + Results.url + " outputted to the bin folder.");
            }

        }

        public static string cleanFileName(string url)
        {
            url = url.Replace("http://", "");
            url = url.Replace("https://", "");
            return url;
        }

    }
}
