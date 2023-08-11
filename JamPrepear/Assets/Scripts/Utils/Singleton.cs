namespace Utils
{
    public class Singleton<T> where T : new()
    {
        private static T _instance;
        private static readonly object syncRoot = new();

        private Singleton() { }

        public static T Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return _instance ??= new T();
                }
            }
        }
    }
}