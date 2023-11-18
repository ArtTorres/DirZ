namespace DirectorySize
{
    internal class DirectoryAnalyzer
    {
        public static IEnumerable<SizeInfo> Analyze(string directoryPath)
        {
            return Analyze(new DirectoryInfo(directoryPath));
        }

        public static IEnumerable<SizeInfo> Analyze(DirectoryInfo directory)
        {
            foreach (var dir in directory.GetDirectories())
            {
                yield return new SizeInfo()
                {
                    Id = dir.Name,
                    Size = Evaluate(dir),
                    Type = FileType.Directory
                };
            }

            foreach (var file in directory.GetFiles())
            {
                yield return new SizeInfo()
                {
                    Id = file.Name,
                    Size = file.Length,
                    Type = FileType.File
                };
            }
        }

        private static long Evaluate(DirectoryInfo directory)
        {
            long inner = 0;

            foreach (var dir in directory.GetDirectories())
            {
                inner = Evaluate(dir);
            }

            return inner + directory.GetFiles().Sum(s => s.Length);
        }
    }
}
