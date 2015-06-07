//#define DEBUG
using CommandLine;
using CommandLine.Text;

namespace OnionScanner {
    class Program {
        
        static void Main(string[] args) {
            Options options = new Options();
            if(CommandLine.Parser.Default.ParseArguments(args, options)) {
                Scanner scanner = new Scanner(options);
#if DEBUG
                int count = 0;
#endif
                if(!string.IsNullOrEmpty(options.InputFile)) {
                    scanner.ScanFile(options.InputFile);
                } else {
                    scanner.ScanYATD();
                    foreach(string url in scanner.rawLinks) {
                        scanner.ScanURL(url);
#if DEBUG
                        count++;
                        if(count > 2) {
                            break;
                        }
#endif
                    }
                }
                scanner.WriteFile();             
            }
        }
    }
}
