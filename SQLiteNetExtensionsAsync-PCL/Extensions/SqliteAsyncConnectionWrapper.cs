using System.Reflection;
using SQLite.Net;
using SQLite.Net.Async;

namespace SQLiteNetExtensionsAsync.Extensions
{
    internal static class SqliteAsyncConnectionWrapper
    {
        private static readonly MethodInfo GetConnectionMethodInfo = typeof(SQLiteAsyncConnection).GetTypeInfo().GetDeclaredMethod("GetConnection");

        static private SQLiteConnectionWithLock GetConnectionWithLock(SQLiteAsyncConnection asyncConnection)
        {
            return (SQLiteConnectionWithLock) GetConnectionMethodInfo.Invoke(asyncConnection, null);
        }

        static public SQLiteConnectionWithLock Lock(SQLiteAsyncConnection asyncConnection)
        {
            return GetConnectionWithLock(asyncConnection);
        }
    }
}