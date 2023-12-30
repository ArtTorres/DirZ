using MagnetArgs;

namespace DirZ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var options = Magnet.Attract<ProgramOptions>(args);

                if (string.IsNullOrEmpty(options.Path))
                {
                    options.Path = System.Environment.CurrentDirectory;
                }

                var analyzer = new Analyzer(options.ShowHidden);
                var report = new Report(analyzer);

                report.Generate(options.Path);
                report.DisplayReport(options.Order, options.Highlight);

                if (options.Verbose)
                {
                    report.DisplayResume();
                    report.DisplayErrors();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
