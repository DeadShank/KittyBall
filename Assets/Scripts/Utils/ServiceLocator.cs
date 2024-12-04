using System.Collections.Generic;

namespace Utils
{
    public static class ServiceLocator
    {
        public static Dictionary<string, object> Items { get; } = new ();

        public static T Get<T>() where T: class => Items.GetValueOrDefault(typeof(T).Name) as T;

        public static void Add<T>(T service) where T: class => Items[typeof(T).Name] = service;
    }
}