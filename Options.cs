using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace OnionScanner {
    class Options {
        [Option('i', "input", Required = false, HelpText = "File to read URLS from")]
        public string InputFile {
            get;
            set;
        }

        [Option('o', "output", Required = true, HelpText = "File to write good URLs to")]
        public string OutputFile {
            get;
            set;
        }

        [Option('v', "verbose", Required = false, DefaultValue = false, HelpText = "File to read URLS from")]
        public bool Verbose {
            get;
            set;
        }

        [Option('u', "proxyurl", Required = true, HelpText = "The URL for the HTTP Proxy to get to TOR [use privoxy]")]
        public string ProxyURL {
            get;
            set;
        }

        [Option('p', "proxyport", Required = true, HelpText = "The port number for the HTTP Proxy to get to TOR [use privoxy]")]
        public int ProxyPort {
            get;
            set;
        }

        [ParserState]
        public IParserState LastParserState {
            get;
            set;
        }

        [HelpOption]
        public string GetUsage() {
            return HelpText.AutoBuild(this,
            (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
