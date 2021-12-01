using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenCivicData
{
    public class OcdIdBuilder
    {
        private static Regex InvalidCharacters { get; } = new(@"[^\w\.\-_~]+");

        private readonly Queue<string> _path = new();

        public OcdIdBuilder Part(string type, string code)
        {
            _path.Enqueue($"{type}:{InvalidCharacters.Replace(code.Replace(" ", "_"), "~")}".ToLower());
            return this;
        }

        public OcdIdBuilder Country(string country)
        {
            return Part("country", country);
        }

        public OcdIdBuilder UnitedStates()
        {
            return Country("us");
        }

        public OcdIdBuilder State(string state)
        {
            var subdivision = state.ToLower() switch
            {
                "dc" => "district",
                "as" => "territory",
                "gu" => "territory",
                "mp" => "territory",
                "pr" => "territory",
                "vi" => "territory",
                _ => "state"
            };

            return Part(subdivision, state);
        }

        public OcdIdBuilder StateLegislativeLowerChamberDistrict(string district)
        {
            return Part("sldl", district);
        }

        public OcdIdBuilder StateLegislativeUpperChamberDistrict(string district)
        {
            return Part("sldu", district);
        }

        public OcdIdBuilder CongressionalDistrict(string district)
        {
            return Part("cd", district);
        }

        public OcdIdBuilder District(string district)
        {
            return Part("district", district);
        }

        public OcdIdBuilder Borough(string borough)
        {
            return Part("borough", borough);
        }

        public OcdIdBuilder County(string county)
        {
            return Part("county", county);
        }

        public OcdIdBuilder Place(string place)
        {
            return Part("place", place);
        }

        public IEnumerable<string> GetPath()
        {
            return _path;
        }
    }
}