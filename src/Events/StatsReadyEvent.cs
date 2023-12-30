namespace DirZ.Events
{
    internal class StatsReadyEvent : EventArgs
    {
        public SizeInfo Size { get; private set; }

        public StatsReadyEvent(SizeInfo size)
        {
            Size = size;
        }
    }
}
