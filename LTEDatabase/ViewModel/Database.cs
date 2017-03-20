using LTEDatabase.Model;

namespace LTEDatabase.ViewModel
{
    class Database
    {
        private static DBContext context;
        private static readonly object locker = new object();

        private Database()
        {
        }

        public static DBContext GetContext()
        {
            lock (locker)
            {
                if (context == null)
                {
                    context = new DBContext();
                }
            }
            return context;
        }

        public static void Close()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }
    }
}