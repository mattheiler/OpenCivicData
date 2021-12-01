using System.Text.RegularExpressions;

namespace OpenCivicData
{
    public class OcdIdArea
    {
        public OcdIdArea(string type, string code)
        {
            Type = type;
            Code = code;
        }

        private static Regex InvalidCharacters { get; } = new(@"[^\w\.\-_~]+");

        public string Type { get; }

        public string Code { get; }

        public override string ToString()
        {
            return $"{Type}:{InvalidCharacters.Replace(Code.Replace(" ", "_"), "~")}";
        }
    }
}