using CommandLine;
using CommandLine.Text;

namespace OnionScanner {
    class Options {
        [Option('i', "input", Required = false, HelpText = "File to read URLs from")]
        public string InputFile {
            get;
            set;
        }

        [Option('o', "output", Required = true, HelpText = "Directory to write files to")]
        public string OutputFile {
            get;
            set;
        }

        [Option('v', "verbose", Required = false, DefaultValue = false, HelpText = "File to read URLs from")]
        public bool Verbose {
            get;
            set;
        }

        [Option('u', "proxyurl", Required = false, DefaultValue = "127.0.0.1", HelpText = "The URL for the HTTP Proxy to get to TOR [use privoxy]")]
        public string ProxyURL {
            get;
            set;
        }

        [Option('p', "proxyport", Required = false, DefaultValue = (int)8118, HelpText = "The port number for the HTTP Proxy to get to TOR [use privoxy]")]
        public int ProxyPort {
            get;
            set;
        }

        [Option('s', "save", Required = false, DefaultValue = false, HelpText = "If set, saves bad and seized links as well")]
        public bool Save {
            get;
            set;
        }

        [Option('d', "desc", Required = false, DefaultValue = false, HelpText = "Show site title with saved good URLs")]
        public bool Description {
            get;
            set;
        }

        [Option('a', "agent", Required = false, DefaultValue = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.4) Gecko/20060508 Firefox/1.5.0.4", HelpText = "User-Agent to emulate")]
        public string Agent {
            get;
            set;
        }

        [Option('n', "numlinks", Required = false, DefaultValue = (int)0, HelpText = "Number of links to scan [setting to 0 will scan all found links]")]
        public int NumLinks {
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
