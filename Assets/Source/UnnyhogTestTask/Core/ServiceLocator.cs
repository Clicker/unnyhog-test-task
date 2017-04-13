using System.Collections.Generic;

namespace UnnyhogTestTask.Core
{
    public static class ServiceLocator
    {
        private static readonly IDictionary<object, object> Services;

        static ServiceLocator()
        {
            Services = new Dictionary<object, object>();
        }

        public static void AddService<T>(T service) where T : class
        {
            Services[typeof(T)] = service;
        }

        public static void RemoveService<T>() where T : class
        {
            Services.Remove(typeof(T));
        }

        public static T GetService<T>() where T : class
        {
            object service;

            if (Services.TryGetValue(typeof(T), out service))
            {
                return (T)service;
            }

            return default(T);
        }
    }
}