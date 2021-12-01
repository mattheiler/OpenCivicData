using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCivicData
{
    public sealed class OcdId
    {
        private readonly string[] _path;
        private readonly string _type;

        private OcdId(string type, IEnumerable<string> path)
        {
            _type = type;
            _path = path.ToArray();
        }

        private OcdId(string type, params string[] path)
            : this(type, path.AsEnumerable())
        {
        }

        public static OcdId Parse(string value)
        {
            return TryParse(value, out var id) ? id : throw new InvalidOperationException("Couldn't parse OCD Id");
        }

        public static bool TryParse(string value, out OcdId id)
        {
            var parts = value.Split('/');
            if (parts.Length < 2)
            {
                id = default;
                return false;
            }

            var part = parts[0];
            if (!part.StartsWith("ocd-"))
            {
                id = default;
                return false;
            }

            id = new OcdId(part[4..], parts.Skip(1).ToArray());
            return true;
        }

        public static OcdId Division(Action<OcdIdBuilder> build)
        {
            var builder = new OcdIdBuilder();
            build(builder);
            return new OcdId("division", string.Join('/', builder.GetPath()));
        }

        public static OcdId Jurisdiction(Action<OcdIdBuilder> build, string classification)
        {
            var builder = new OcdIdBuilder();
            build(builder);
            return new OcdId("jurisdiction", string.Join('/', builder.GetPath().Append(classification)));
        }

        public static OcdId Government(Action<OcdIdBuilder> build)
        {
            return Jurisdiction(build, "government");
        }

        public static OcdId Legislature(Action<OcdIdBuilder> build)
        {
            return Jurisdiction(build, "legislature");
        }

        public static OcdId Executive(Action<OcdIdBuilder> build)
        {
            return Jurisdiction(build, "executive");
        }

        public static OcdId SchoolSystem(Action<OcdIdBuilder> build)
        {
            return Jurisdiction(build, "school_system");
        }

        public static OcdId TransitAuthority(Action<OcdIdBuilder> build)
        {
            return Jurisdiction(build, "transit_authority");
        }

        public static OcdId Organization(Guid id)
        {
            return new OcdId("organization", id.ToString());
        }

        public static OcdId Person(Guid id)
        {
            return new OcdId("person", id.ToString());
        }

        public override string ToString()
        {
            return $"ocd-{_type}/{string.Join('/', _path)}".ToLower();
        }

        public static implicit operator string(OcdId id)
        {
            return id.ToString();
        }
    }
}