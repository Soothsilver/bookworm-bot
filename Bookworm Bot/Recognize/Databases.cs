using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Bookworm.Recognize
{
    class Databases
    {
        public static string LETTERDB_FILEPATH = "letterdb.dat";
        public static string LETTERDB_BACKUP_FILEPATH = "letterdb.dat.bak";
    
        internal static Database Load(string filepath)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                Database database = (Database)bf.Deserialize(fs);
                fs.Flush();
                return database;
            }
        }
        internal static void Save(Database database, string filepath)
        {
            File.Copy(filepath, LETTERDB_BACKUP_FILEPATH, true);
            using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, database);
                fs.Flush();
            }
        }
    }
}
