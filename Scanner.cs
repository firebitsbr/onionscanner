//#define DEBUG
using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using CommandLine;
using CommandLine.Text;

namespace OnionScanner {
    class Scanner {
        protected List<string> seizedSites;
        protected List<string> removedSites;
        protected List<string> goodSites;
        public List<string> rawLinks;
        protected Options options;

        public Scanner(Options opt) {
            options = opt;
            goodSites = new List<string>();
            rawLinks = new List<string>();

            if(options.Save) {
                seizedSites = new List<string>();
                removedSites = new List<string>();
            }
        }

        //Scans URLs from file
        public void ScanFile(string filename) {
            string line;
            int count = 0;

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
                if(options.NumLinks > 0) {
                    count++;
                    if(count >= options.NumLinks) {
                        break;
                    }
                }
            }
            file.Close();
        }

        //Scans URL and determines whether it is good, bad, or seized
        public void ScanURL(string url) {
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
                            if(options.Save) {
                                seizedSites.Add(url);
                            }
                        } else {
                            //Page is still alive
                            if(options.Verbose) {
                                Console.WriteLine("Site Good: " + url);
                            }

                            if(options.Description) {
                                HtmlDocument doc = new HtmlDocument();
                                doc.LoadHtml(html);

                                HtmlNodeCollection titlenode = doc.DocumentNode.SelectNodes("//title");
                                HtmlNode title = titlenode[0];
                                string t = title.InnerText;
                                goodSites.Add(t + " - " + url);
                            } else {
                                goodSites.Add(url);
                            }
                        }
                    } else {//Site did not return a 200 status...must be bad
                        if(options.Verbose) {
                            Console.WriteLine("Site Bad: " + url);
                        }
                        if(options.Save) {
                            removedSites.Add(url);
                        }
                    }
                    response.Close();
                    return;
                } catch(System.Net.WebException e) { //Site returned a 5** status...must be bad
                    if(options.Verbose) {
                        Console.WriteLine("Site Bad: " + url);
                    }
                    if(options.Save) {
                        removedSites.Add(url);
                    }
                }
            }
        }

        //Gets links from YATD and scans them
        public void ScanYATD() {
            string yatdurl = "http://bdpuqvsqmphctrcs.onion/noscript.html";
            int count = 0;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(yatdurl);
            request.UserAgent = @options.Agent;
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
                    string link = node.Attributes["href"].Value;
                    rawLinks.Add(link);
                    if(options.Verbose) {
                        Console.WriteLine("Found Link: " + link);
                    }
                    if(options.NumLinks > 0) {
                        count++;
                        if(count >= options.NumLinks) {
                            break;
                        }
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
        public void WriteFile() {
            if(goodSites.Count > 0) {
                StreamWriter goodlinks = new StreamWriter(options.OutputFile + "links_good.txt");
                StreamWriter badlinks = new StreamWriter(options.OutputFile + "links_bad.txt");
                StreamWriter seizedlinks = new StreamWriter(options.OutputFile + "links_seized.txt");

                if(options.Verbose) {
                    Console.WriteLine("Writing good links to file");
                }

                foreach(string url in goodSites) {
                    goodlinks.WriteLine(url);
                }
                goodlinks.Close();

                if(options.Save) {
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
                }
            }
            return;
        }
    }
}
