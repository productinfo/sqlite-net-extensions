using System.IO;
using System.Linq;
using Foundation;

#if USING_MVVMCROSS
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinIOS;
#else
using SQLite;
#endif

namespace SQLiteNetExtensions.IntegrationTests
{
    /// <summary>
    /// Helper methods for the integration tests. 
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Returns the proper database file path to initialize the SQLite connection. 
        /// </summary>
        public static string DatabaseFilePath
        {
            get
            {
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
				var urls = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.LibraryDirectory, NSSearchPathDomain.User);
	            var libraryPath = urls.First().Path;
                return Path.Combine(libraryPath, "database.db3");
            }
        }

        public static SQLiteConnection CreateConnection() {
#if USING_MVVMCROSS
			return new SQLiteConnection(new SQLitePlatformIOS(), DatabaseFilePath);
#else
			return new SQLiteConnection(DatabaseFilePath);
#endif
		}

		public static SQLiteAsyncConnection CreateAsyncConnection()
		{
#if USING_MVVMCROSS
			var connectionString = new SQLiteConnectionString(DatabaseFilePath, false);
			var connectionWithLock = new SQLiteConnectionWithLock(new SQLitePlatformIOS(), connectionString);
			return new SQLiteAsyncConnection(() => connectionWithLock);
#else
			return new SQLiteAsyncConnection(DatabaseFilePath);
#endif
		}
	}
}