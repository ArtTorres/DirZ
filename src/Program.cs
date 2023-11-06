using MagnetArgs;

namespace DirectorySize
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                char symbol = Environment.OSVersion.ToString().Contains("win", StringComparison.OrdinalIgnoreCase) ? '/' : '-';
                var options = Magnet.Attract<ProgramOptions>(args, symbol);

                if (string.IsNullOrEmpty(options.Path))
                    options.Path = System.Environment.CurrentDirectory;

                var items = DirectoryAnalyzer.Analyze(options.Path);

                Report.Print(
                    options.Order == Order.Default ? items : 
                        options.Order == Order.Descending ? items.OrderByDescending(s => s.Size) : items.OrderBy(s => s.Size),
                    options.Highlight
                );
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
