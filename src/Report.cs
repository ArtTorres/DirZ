using DirZ.Events;
using DirZ.Util;

namespace DirZ
{
    internal class Report
    {
        private Analyzer _analyzer;
        private List<Exception> _errors;
        private List<SizeInfo> _results;

        public Report(Analyzer fileAnalyzer)
        {
            _analyzer = fileAnalyzer;
            _errors = new List<Exception>();
            _results = new List<SizeInfo>();

            _analyzer.StatsReady+=_analyzer_StatsReady;
            _analyzer.AccessDenied+=FileAnalyzer_AccessDenied;
        }

        public void Generate(string path)
        {
            _analyzer.Analyze(path);
        }

        public void DisplayResume()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine($"Files processed [{_analyzer.FilesProcessed}]");
        }

        public void DisplayReport(Order order = Order.Default, bool highlight = false)
        {
            var systemColor = Console.ForegroundColor;

            var items = order == Order.Default ? 
                                    _results.ToList() : order == Order.Descending ? 
                                    _results.OrderByDescending(s => s.Size).ToList() : _results.OrderBy(s => s.Size).ToList();

            foreach (var item in items)
            {
                int unitIndex = 0;
                decimal size = SizeFormatTool.Compact(item.Size, ref unitIndex);

                if (highlight)
                    Console.ForegroundColor = SizeFormatTool.GetUnitColor(unitIndex);

                Console.Write(
                    "{0,9}\t",
                    $"{size:0.00} {SizeFormatTool.GetUnit(unitIndex)}"
                );

                if (highlight)
                    Console.ForegroundColor = item.Type == FileType.Directory ? ConsoleColor.Yellow : ConsoleColor.White;

                Console.WriteLine(
                    "{0}",
                    item.Type == FileType.Directory ? highlight ? $"{item.Id}" : $"[{item.Id}]" : $"{item.Id}"
                );
            }

            if (highlight)
                Console.ForegroundColor = systemColor;
        }

        public void DisplayErrors()
        {
            Console.WriteLine($"Identified errors [{_errors.Count}]");

            foreach (Exception e in _errors)
            {
                Console.WriteLine($"{e.Message}");
            }
        }

        #region Events

        private void _analyzer_StatsReady(object sender, Events.StatsReadyEvent e)
        {
            _results.Add(e.Size);
        }

        private void FileAnalyzer_AccessDenied(object sender, AccessDeniedEvent e)
        {
            _errors.Add(e.Exception);
        }

        #endregion
    }
}
