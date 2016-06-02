using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSeeker
{
    internal class ServiceKey : IEquatable<ServiceKey>
    {
        public ServiceKey(Type Type, string Name)
        {
            type = Type;
            name = Name;
        }
        private Type type;
        private string name;

        public Type Type => type;
        public string Name => name;

        public override int GetHashCode()
        {
            unchecked
            {
                const int multiplier = 48611;
                int hash = 104723;
                hash = (hash * multiplier) ^ (Type?.GetHashCode() ?? 0);
                hash = (hash * multiplier) ^ (Name?.GetHashCode() ?? 0);
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ServiceKey);
        }

        public bool Equals(ServiceKey key)
        {
            return (!Object.ReferenceEquals(key, null) &&
                Object.ReferenceEquals(this, key)) ||
                (Type.Equals(key.Type) &&
                Name.Equals(key.Name));
        }

        public static bool operator ==(ServiceKey key1, ServiceKey key2)
        {
            if (Object.ReferenceEquals(key1, null))
            {
                return key1.Equals(key2);
            }
            else
            {
                return key2.Equals(key1);
            }
        }

        public static bool operator !=(ServiceKey key1, ServiceKey key2)
        {
            return !(key1 == key2);
        }
    }
}
