using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCivicData
{
    public sealed class OcdId
    {
        private readonly object[] _data;
        private readonly string _prefix;

        private OcdId(string prefix, IEnumerable<object> data)
            : this(prefix, data.ToArray())
        {
        }

        private OcdId(string prefix, params object[] data)
        {
            _prefix = prefix;
            _data = data;
        }

        public static OcdId Division(Action<OcdIdAreaBuilder> build)
        {
            var areas = new OcdIdAreaBuilder();
            build(areas);
            return new OcdId("ocd-division", string.Join('/', areas.GetAreas()));
        }

        public static OcdId Jurisdiction(Action<OcdIdAreaBuilder> build, string classification)
        {
            var areas = new OcdIdAreaBuilder();
            build(areas);
            return new OcdId("ocd-jurisdiction", string.Join('/', areas.GetAreas().OfType<object>().Append(classification)));
        }

        public static OcdId Government(Action<OcdIdAreaBuilder> build)
        {
            return Jurisdiction(build, "government");
        }

        public static OcdId Legislature(Action<OcdIdAreaBuilder> build)
        {
            return Jurisdiction(build, "legislature");
        }

        public static OcdId Executive(Action<OcdIdAreaBuilder> build)
        {
            return Jurisdiction(build, "executive");
        }

        public static OcdId SchoolSystem(Action<OcdIdAreaBuilder> build)
        {
            return Jurisdiction(build, "school_system");
        }

        public static OcdId TransitAuthority(Action<OcdIdAreaBuilder> build)
        {
            return Jurisdiction(build, "transit_authority");
        }

        public static OcdId Organization(Guid id)
        {
            return new OcdId("ocd-organization", id.ToString());
        }

        public static OcdId Person(Guid id)
        {
            return new OcdId("ocd-person", id.ToString());
        }

        public override string ToString()
        {
            return $"{_prefix}/{string.Join('/', _data)}".ToLower();
        }

        public static implicit operator string(OcdId id)
        {
            return id.ToString();
        }
    }
}