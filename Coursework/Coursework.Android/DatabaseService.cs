using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Coursework.DSInterfaces;
using SQLite;



[assembly: Xamarin.Forms.Dependency(typeof(Coursework.Droid.DatabaseService))]
namespace Coursework.Droid
{
    class DatabaseService : IDatabase
    {
        public DatabaseService(){}

        public SQLiteConnection CreateConnection()
        {
            var sqliteFilename = "Constructions.db";
            string documentsDirectoryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsDirectoryPath, sqliteFilename);

            // This is where we copy in our pre-created database
#warning "RETURN '!' IN File.Exists(path) !!!!!"
            if (!File.Exists(path))
            {
                using (var binaryReader = new BinaryReader(Application.Context.Assets.Open(sqliteFilename)))
                {
                    using (var binaryWriter = new BinaryWriter(new FileStream(path, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = binaryReader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            binaryWriter.Write(buffer, 0, length);
                        }
                    }
                }
            }

                var conn = new SQLiteConnection(path);

            return conn;
        }
    }
}