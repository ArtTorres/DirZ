namespace DirectorySize
{
    internal static class Report
    {
        private static string[] _units = new string[] { "B", "kB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        private static ConsoleColor[] _colors = new ConsoleColor[] {
            ConsoleColor.Gray,
            ConsoleColor.Cyan,
            ConsoleColor.Yellow,
            ConsoleColor.White,
            ConsoleColor.Green,
            ConsoleColor.Blue,
            ConsoleColor.Magenta,
            ConsoleColor.Red,
            ConsoleColor.DarkBlue
        };

        public static void Print(IEnumerable<SizeInfo> items, bool highlight = false)
        {
            var systemColor = Console.ForegroundColor;

            foreach (var item in items)
            {
                Console.WriteLine(
                    "{0,9}\t{1}",
                    Compact(item.Size, highlight),
                    item.Id
                );
            }

            if (highlight)
                Console.ForegroundColor = systemColor;
        }

        private static string Compact(long size, bool highlight)
        {
            int c = 0;
            decimal s = Shrink(size, ref c);

            if (highlight)
                Console.ForegroundColor = _colors[c];

            return $"{s:0.00} {_units[c]}";

            decimal Shrink(decimal value, ref int cycles)
            {
                if (value >= 1000)
                {
                    cycles++;

                    return Shrink(value / 1024m, ref cycles);
                }
                else
                {
                    return value;
                }
            }
        }
    }
}
