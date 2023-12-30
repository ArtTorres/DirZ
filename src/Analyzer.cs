using DirZ.Events;

namespace DirZ
{
    internal class Analyzer
    {
        public void OnStatsReady(SizeInfo info)
        {
            StatsReady?.Invoke(this, new StatsReadyEvent(info));
        }
        public event EventHandler<StatsReadyEvent> StatsReady;


        public void OnAccessDenied(Exception ex)
        {
            AccessDenied?.Invoke(this, new AccessDeniedEvent(ex));
        }
        public event EventHandler<AccessDeniedEvent> AccessDenied;

        public long FilesProcessed { get { return _count; } }
        private long _count;

        private bool _showHidden;

        public Analyzer(bool showHidden = false)
        {
            _showHidden = showHidden;
        }

        public void Analyze(string path)
        {
            var directory = new DirectoryInfo(path);

            if (directory.Exists)
            {
                Analyze(directory);
            }
            else
            {
                Analyze(new DriveInfo(path));
            }
        }

        public void Analyze(DriveInfo drive)
        {
            try
            {
                foreach (var dir in drive.RootDirectory.GetDirectories())
                {
                    Eval(dir);
                }

                Eval(drive.RootDirectory.GetFiles());
            }
            catch (Exception ex)
            {
                OnAccessDenied(ex);
            }
        }

        public void Analyze(DirectoryInfo directory)
        {
            try
            {
                foreach (var dir in directory.GetDirectories())
                {
                    Eval(dir);
                }

                Eval(directory.GetFiles());
            }
            catch (Exception ex)
            {
                OnAccessDenied(ex);
            }
        }

        private void Eval(DirectoryInfo directory)
        {
            if (_showHidden || !directory.Attributes.HasFlag(FileAttributes.Hidden))
            {
                try
                {
                    OnStatsReady(new SizeInfo()
                    {
                        Id = directory.Name,
                        Size = (
                            Calc(directory.GetDirectories()) +
                            Calc(directory.GetFiles())
                        ),
                        Type = FileType.Directory
                    });
                }
                catch (Exception ex)
                {
                    OnAccessDenied(ex);
                }
            }
        }

        private void Eval(IEnumerable<FileInfo> files)
        {
            foreach (FileInfo file in files)
            {
                if (_showHidden || !file.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    try
                    {

                        OnStatsReady(new SizeInfo()
                        {
                            Id = file.Name,
                            Size = file.Length,
                            Type = FileType.File
                        });

                    }
                    catch (Exception ex)
                    {
                        OnAccessDenied(ex);
                    }
                    finally
                    {
                        _count++;
                    }
                }
            }
        }

        private long Calc(IEnumerable<DirectoryInfo> directories)
        {
            long output = 0;

            foreach (DirectoryInfo directory in directories)
            {
                if (_showHidden || !directory.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    try
                    {
                        output += Calc(directory.GetDirectories());

                        output += Calc(directory.GetFiles());
                    }
                    catch (Exception ex)
                    {
                        OnAccessDenied(ex);
                    }
                }
            }

            return output;
        }

        private long Calc(IEnumerable<FileInfo> files)
        {
            long output = 0;

            foreach (FileInfo file in files)
            {
                if (_showHidden || !file.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    try
                    {
                        output += file.Length;
                    }
                    catch (Exception ex)
                    {
                        OnAccessDenied(ex);
                    }
                    finally
                    {
                        _count++;
                    }
                }
            }

            return output;
        }
    }
}
