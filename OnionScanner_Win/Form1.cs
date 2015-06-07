using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using HtmlAgilityPack;
using System.IO;
using System.Net;

namespace OnionScanner_Win {
    public partial class Form1 : Form {
        private string curDir;
        private string inputFilePath;

        protected List<string> seizedSites;
        protected List<string> removedSites;
        protected List<string> goodSites;
        public List<string> rawLinks;

        private string outputDir;

        public Form1() {
            InitializeComponent();
            curDir = System.IO.Directory.GetCurrentDirectory();

            goodSites = new List<string>();
            rawLinks = new List<string>();
            seizedSites = new List<string>();
            removedSites = new List<string>();

            status_label.Text = "Select your settings and hit 'Scan'";
            outputDir = "";

        }

        private void Form1_Load(object sender, EventArgs e) {
            
        }

        private void runCommand() {

            if(file_radio.Checked) {
                ScanFile(inputFilePath);
            } else {
                ScanYATD();
            }

            foreach(string url in rawLinks) {
               ScanURL(url);
            }

            WriteFile();
            scan.Enabled = true;
            progressbar.Value = 100;
        }

        #region Scanner
        //Write links to files
        public void WriteFile() {
            if(goodSites.Count > 0) {
                StreamWriter goodlinks = new StreamWriter(outputDir + "\\links_good.txt");
                StreamWriter badlinks = new StreamWriter(outputDir + "\\links_bad.txt");
                StreamWriter seizedlinks = new StreamWriter(outputDir + "\\links_seized.txt");

                if(verbose.Checked) {
                    WriteOutput("Writing good links to file");
                }

                foreach(string url in goodSites) {
                    goodlinks.WriteLine(url);
                }
                goodlinks.Close();

                if(saveBad.Checked) {
                    if(verbose.Checked) {
                        WriteOutput("Writing bad links to file");
                    }

                    foreach(string url in removedSites) {
                        badlinks.WriteLine(url);
                    }
                    badlinks.Close();

                    if(verbose.Checked) {
                        WriteOutput("Writing seized links to file");
                    }

                    foreach(string url in seizedSites) {
                        seizedlinks.WriteLine(url);
                    }
                    seizedlinks.Close();
                }
            }
            return;
        }

        //Gets links from YATD and scans them
        public void ScanYATD() {
            if(verbose.Checked) {
                WriteOutput("Scanning YATD...");
            }

            string yatdurl = "http://bdpuqvsqmphctrcs.onion/noscript.html";
            int count = 0;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(yatdurl);
            request.UserAgent = @useragent_text.Text;
            WebProxy myproxy = new WebProxy(proxyUrl_text.Text, Convert.ToInt32(proxyPort_text.Text));
            myproxy.BypassProxyOnLocal = false;
            request.Proxy = myproxy;
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //If the website is up and working let's check if it's header is ok
            if(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted) {
                var stream = response.GetResponseStream();
                var reader = new StreamReader(stream);
                var html = reader.ReadToEnd();

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                doc.LoadHtml(html);

                foreach(HtmlNode node in doc.DocumentNode.SelectNodes("//table[@id='example_noscript']/tbody/tr/td[2]/a")) {
                    string link = node.Attributes["href"].Value;
                    rawLinks.Add(link);
                    if(verbose.Checked) {
                        WriteOutput("Found Link: " + link);
                    }
                    if(Convert.ToInt32(numlinks_text.Text) > 0) {
                        count++;
                        if(count >= Convert.ToInt32(numlinks_text.Text)) {
                            break;
                        }
                    }
                }

                if(verbose.Checked) {
                    WriteOutput("Done searching YATD, now on to scanning links");
                }

                return;

            } else {
                Console.WriteLine("ERROR! COULD NOT OPEN YATD!");
                System.Threading.Thread.Sleep(5000);
                System.Environment.Exit(1);
            }
        }

        //Scans URL and determines whether it is good, bad, or seized
        public void ScanURL(string url) {
            if(url.Length > 0) {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.4) Gecko/20060508 Firefox/1.5.0.4";
                WebProxy myproxy = new WebProxy(proxyUrl_text.Text, Convert.ToInt32(proxyPort_text.Text));
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
                            if(verbose.Checked) {
                                WriteOutput("Site Seized: " + url);
                            }
                            if(saveBad.Checked) {
                                seizedSites.Add(url);
                            }
                        } else {
                            //Page is still alive
                            if(verbose.Checked) {
                                WriteOutput("Site Good: " + url);
                            }

                            if(desc.Checked) {
                                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
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
                        if(verbose.Checked) {
                            WriteOutput("Site Bad: " + url);
                        }
                        if(saveBad.Checked) {
                            removedSites.Add(url);
                        }
                    }
                    response.Close();
                    return;
                } catch(System.Net.WebException e) { //Site returned a 5** status...must be bad
                    if(verbose.Checked) {
                        WriteOutput("Site Bad: " + url);
                    }
                    if(saveBad.Checked) {
                        removedSites.Add(url);
                    }
                }

                progressbar.Value = (rawLinks.FindIndex(delegate(string s) {
                    return s == url;
                }) / rawLinks.Count)*100;
            }
        }

        //Scans URLs from file
        public void ScanFile(string filename) {
            string line;
            int count = 0;

            if(verbose.Checked) {
                WriteOutput("Opening File: " + filename);
            }

            if(!File.Exists(filename)) {
                Console.WriteLine("ERROR! FILE DOES NOT EXIST AT " + filename);
                System.Threading.Thread.Sleep(5000);
                System.Environment.Exit(1);
            }

            StreamReader file = new StreamReader(filename);
            while((line = file.ReadLine()) != null) {
                ScanURL(line);
                if(Convert.ToInt32(numlinks_text.Text) > 0) {
                    count++;
                    if(count >= Convert.ToInt32(numlinks_text.Text)) {
                        break;
                    }
                }
            }
            file.Close();
        }

        #endregion
        private void WriteOutput(string mesg) {
            output.Text += mesg + "\r\n";
        }

        #region Interface

        private void scan_Click(object sender, EventArgs e){
            status_label.Text = "Select output directory";
            progressbar.Value = 1;

            FolderBrowserDialog saveDirDiag = new FolderBrowserDialog();
            if(saveDirDiag.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                outputDir = saveDirDiag.SelectedPath;
                WriteOutput("Output Dir Set:" + outputDir);
                scan.Enabled = false;
            }

            runCommand();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            output.Text="";
        }

        private void file_radio_CheckedChanged(object sender, EventArgs e) {
            if(file_radio.Checked) {
                OpenFileDialog openFileDiag = new OpenFileDialog();
                if(openFileDiag.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    inputFilePath = openFileDiag.FileName;
                    WriteOutput("Input File Set:" + inputFilePath);
                }
            }
        }

        #endregion

        private void output_TextChanged(object sender, EventArgs e) {
            output.SelectionStart = output.Text.Length;
            output.ScrollToCaret();
        }
    }
}
