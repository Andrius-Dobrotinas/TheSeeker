using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSeeker
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IDictionary<ServiceKey, Type> collection = new Dictionary<ServiceKey, Type>();
        public T Get<T>() where T : class
        {
            return Get<T>(null);
        }

        public T Get<T>(string name) where T : class
        {
            ServiceKey key = new ServiceKey(typeof(T), name);

            if (!collection.Keys.Contains(key))
            {
                throw new ArgumentOutOfRangeException(key.Name, "The type has not been registered with this repository");
            }
            return (T)Activator.CreateInstance(collection[key]);
        }

        public void Register<T>(Type implementation) where T : class
        {
            Register<T>(implementation, null);
        }

        public void Register<T>(Type implementation, string name) where T : class
        {
            ServiceKey key = new ServiceKey(typeof(T), name);
            collection.Add(key, implementation);
        }
    }
}
