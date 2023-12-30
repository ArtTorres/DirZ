namespace DirZ.Util
{
    internal static class SizeFormatTool
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

        public static string GetUnit(int index)
        {
            return _units[index];
        }

        public static ConsoleColor GetUnitColor(int index)
        {
            return _colors[index];
        }

        public static decimal Compact(long size, ref int index)
        {
            int c = 0;
            decimal s = Shrink(size, ref c);

            index = c;
            return s;

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
