using MagnetArgs;
using MagnetArgs.Parsers;

namespace DirectorySize
{
    [Magnetizable]
    internal class ProgramOptions
    {
        [Argument("path", Alias = "p")]
        public string Path { get; set; }

        [Argument("highlight", Alias = "h"), IfPresent]
        public bool Highlight { get; set; }

        [Argument("order", Alias = "o"), Parser(typeof(OrderParser))]
        public Order Order { get; set; }
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
            return value.StartsWith("z", StringComparison.OrdinalIgnoreCase) 
                ? Order.Descending : Order.Ascending;
        }
    }
}
