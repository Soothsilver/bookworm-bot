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
        public static string ATTACKPOSSIBLE_FILEPATH = "attackPossible.dat";
        public static string ATTACKIMPOSSIBLE_FILEPATH = "attackImpossible.dat";

        private void SerializeLetterDB()
        {
            using (System.IO.FileStream s = new System.IO.FileStream("letterdb.dat", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
              //  bf.Serialize(s, LetterDB);
                s.Flush();
            }
        }

        internal static Database Load()
        {
            using (FileStream fs = new FileStream(LETTERDB_FILEPATH, FileMode.OpenOrCreate, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                Database database = (Database)bf.Deserialize(fs);
                fs.Flush();
                return database;
            }
        }
    }
}
