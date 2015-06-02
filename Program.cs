using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;

using System.IO;

using HtmlAgilityPack;

using CommandLine;
using CommandLine.Text;

namespace OnionScanner {
    class Program {
        static Options options;
        static List<string> seizedSites;
        static List<string> removedSites;
        static List<string> goodSites;
        static List<string> rawLinks;
 
        static void Main(string[] args) {
            seizedSites = new List<string>();
            removedSites = new List<string>();
            goodSites = new List<string>();
            rawLinks = new List<string>();

            int count = 0;

            options = new Options();
            if(CommandLine.Parser.Default.ParseArguments(args, options)) {

                if(!string.IsNullOrEmpty(options.InputFile)) {
                    ScanFile(options.InputFile);
                } else {
                    ScanYATD();

                    foreach(string url in rawLinks) {
                        ScanURL(url);
                        count++;
#if DEBUG
                        if(count > 2) {
                            break;
                        }
#endif
                    }
                }
                WriteFile();
                if(options.Verbose) {
                    Console.WriteLine("Done!");
                    System.Threading.Thread.Sleep(5000);
                    System.Environment.Exit(1);
                }
            }

            Console.WriteLine("Done");

        }

        //Scans URLs from file
        static void ScanFile(string filename) {
            string line;

            if(options.Verbose) {
                Console.WriteLine("Opening File: " + filename);
            }

            if(!File.Exists(filename)) {
                Console.WriteLine("ERROR! FILE DOES NOT EXIST AT " + filename);
                System.Threading.Thread.Sleep(5000);
                System.Environment.Exit(1);
            }

            StreamReader file = new StreamReader(filename);
            while((line = file.ReadLine()) != null) {
                ScanURL(line);
            }
            file.Close();
        }

        //Scans URL and determines whether it is good, bad, or seized
        static void ScanURL(string url) {
            if(url.Length > 0) {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.4) Gecko/20060508 Firefox/1.5.0.4";
                WebProxy myproxy = new WebProxy(options.ProxyURL, options.ProxyPort);
                myproxy.BypassProxyOnLocal = false;
                request.Proxy = myproxy;
                request.Method = "GET";

                try {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    //If the website is up and working let's check if it's header is ok
                    if(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted) {
                        var stream = response.GetResponseStream();
                        var reader = new StreamReader(stream);
                        var html = reader.ReadToEnd();

                        if(html.Contains("<title>Alert!</title>")) {
                            //Page has been taken down, and contains nothing interesting/useful
                            if(options.Verbose) {
                                Console.WriteLine("Site Seized: " + url);
                            }
                            seizedSites.Add(url);
                        } else {
                            //Page is still alive
                            if(options.Verbose) {
                                Console.WriteLine("Site Good: " + url);
                            }
                            goodSites.Add(url);
                        }
                    } else {
                        if(options.Verbose) {
                            Console.WriteLine("Site Bad: " + url);
                        }
                        removedSites.Add(url);
                    }
                    response.Close();
                    return;
                } catch(System.Net.WebException e) {
                    if(options.Verbose) {
                        Console.WriteLine("Site Bad: " + url);
                    }
                    removedSites.Add(url);
                }
            }
        }

        //Gets links from YATD and scans them
        static void ScanYATD() {
            string yatdurl = "http://bdpuqvsqmphctrcs.onion/noscript.html";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(yatdurl);
            request.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.4) Gecko/20060508 Firefox/1.5.0.4";
            WebProxy myproxy = new WebProxy(options.ProxyURL, options.ProxyPort);
            myproxy.BypassProxyOnLocal = false;
            request.Proxy = myproxy;
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //If the website is up and working let's check if it's header is ok
            if(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted) {
                var stream = response.GetResponseStream();
                var reader = new StreamReader(stream);
                var html = reader.ReadToEnd();

                HtmlDocument doc = new HtmlDocument();

                doc.LoadHtml(html);

                foreach(HtmlNode node in doc.DocumentNode.SelectNodes("//table[@id='example_noscript']/tbody/tr/td[2]/a")) {
                    Console.WriteLine(node.InnerHtml);
                    string link = node.Attributes["href"].Value;
                    rawLinks.Add(link);
                    if(options.Verbose) {
                        Console.WriteLine("Found Link: " + link);
                    }
                }

                if(options.Verbose) {
                    Console.WriteLine("Done searching YATD, now on to scanning links");
                }

                return;

             } else {
                Console.WriteLine("ERROR! COULD NOT OPEN YATD!");
                System.Threading.Thread.Sleep(5000);
                System.Environment.Exit(1);
             }
        }

        //Write links to files
        static void WriteFile() {
            StreamWriter goodlinks = new StreamWriter(options.OutputFile);
            StreamWriter badlinks = new StreamWriter(options.OutputFile + "_bad");
            StreamWriter seizedlinks = new StreamWriter(options.OutputFile + "_seized");

            if(options.Verbose) {
                Console.WriteLine("Writing good links to file");
            }

            foreach(string url in goodSites) {
                goodlinks.WriteLine(url);
            }
            goodlinks.Close();

            if(options.Verbose) {
                Console.WriteLine("Writing bad links to file");
            }

            foreach(string url in removedSites) {
                badlinks.WriteLine(url);
            }
            badlinks.Close();

            if(options.Verbose) {
                Console.WriteLine("Writing seized links to file");
            }

            foreach(string url in seizedSites) {
                seizedlinks.WriteLine(url);
            }
            seizedlinks.Close();
            return;
        }
    }
}
