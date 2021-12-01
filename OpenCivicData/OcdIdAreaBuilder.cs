using System.Collections.Generic;

namespace OpenCivicData
{
    public class OcdIdAreaBuilder
    {
        private readonly Queue<OcdIdArea> _areas = new();

        public OcdIdAreaBuilder Area(string type, string code)
        {
            _areas.Enqueue(new OcdIdArea(type, code));
            return this;
        }

        public OcdIdAreaBuilder Country(string country)
        {
            return Area("country", country);
        }

        public OcdIdAreaBuilder UnitedStates()
        {
            return Country("us");
        }

        public OcdIdAreaBuilder State(string state)
        {
            var subdivision = state switch
            {
                "DC" => "district",
                "AS" => "territory",
                "GU" => "territory",
                "MP" => "territory",
                "PR" => "territory",
                "VI" => "territory",
                _ => "state"
            };

            return Area(subdivision, state);
        }

        public OcdIdAreaBuilder StateLegislativeLowerChamberDistrict(string district)
        {
            return Area("sldl", district);
        }

        public OcdIdAreaBuilder StateLegislativeUpperChamberDistrict(string district)
        {
            return Area("sldu", district);
        }

        public OcdIdAreaBuilder CongressionalDistrict(string district)
        {
            return Area("cd", district);
        }

        public OcdIdAreaBuilder District(string district)
        {
            return Area("district", district);
        }

        public OcdIdAreaBuilder Borough(string borough)
        {
            return Area("borough", borough);
        }

        public OcdIdAreaBuilder County(string county)
        {
            return Area("county", county);
        }

        public OcdIdAreaBuilder Place(string place)
        {
            return Area("place", place);
        }

        public IEnumerable<OcdIdArea> GetAreas()
        {
            return _areas;
        }
    }
}