using MagnetArgs;
using MagnetArgs.Parsers;

namespace DirZ
{
    [Magnetizable]
    internal class ProgramOptions
    {
        [Argument("path", Alias = "p")]
        public string Path { get; set; }

        [Argument("highlight", Alias = "h"), IfPresent]
        public bool Highlight { get; set; }

        [Argument("sort", Alias = "s"), Parser(typeof(OrderParser))]
        public Order Order { get; set; }

        [Argument("show-hidden", Alias = "sh"), IfPresent]
        public bool ShowHidden {  get; set; }

        [Argument("verbose", Alias = "v"), IfPresent]
        public bool Verbose { get; set; }
    }

    internal enum Order
    {
        Default,
        Ascending,
        Descending
    }

    internal class OrderParser : IParser
    {
        public object Parse(string value)
        {
            return value.StartsWith("des", StringComparison.OrdinalIgnoreCase)
                ? Order.Descending : value.StartsWith("asc", StringComparison.OrdinalIgnoreCase) ? 
                  Order.Ascending : Order.Default;
        }
    }
}
