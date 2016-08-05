using System;
using System.Reflection;
#if !USING_MVVMCROSS
using SQLite;
using SQLiteConnectionWithLock = SQLite.SQLiteConnection;
#endif

namespace SQLiteNetExtensionsAsync.Extensions
{
    public static class SqliteAsyncConnectionWrapper
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

#if !USING_MVVMCROSS
	public static class SqliteConnectionExtensions
    {
        static public IDisposable Lock(this SQLiteConnectionWithLock connection)
        {
            var lockMethod = connection.GetType().GetTypeInfo().GetDeclaredMethod("Lock");
            return (IDisposable)lockMethod.Invoke(connection, null);
        }
    }
#endif
}