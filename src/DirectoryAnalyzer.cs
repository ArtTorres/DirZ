using System.IO;

namespace DirectorySize
{
    internal class DirectoryAnalyzer
    {
        public static IEnumerable<SizeInfo> Analyze(string directoryPath)
        {
            var directory = new DirectoryInfo(directoryPath);

            if (directory.Exists)
            {
                return Analyze(directory);
            }
            else
            {
                var drive = new DriveInfo(directoryPath);
                return Analyze(drive);
            }
        }

        public static IEnumerable<SizeInfo> Analyze(DriveInfo drive)
        {
            foreach (var dir in drive.RootDirectory.GetDirectories())
            {
                yield return new SizeInfo()
                {
                    Id = dir.Name,
                    Size = Evaluate(dir),
                    Type = FileType.Directory
                };
            }

            foreach (var file in drive.RootDirectory.GetFiles())
            {
                yield return new SizeInfo()
                {
                    Id = file.Name,
                    Size = file.Length,
                    Type = FileType.File
                };
            }
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
