using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookworm.Scan;

namespace Bookworm.Recognize
{
    public class DatabaseManager
    {

        public object DatabaseLock { get; } = new object();
        public Database Database { get; set; }
        public string Filename { get; set; }

        internal void SaveAllSnapshotLettersIntoDatabase(Snapshot snapshot)
        {
            lock (DatabaseLock)
            {
                foreach (SnapshotLetter letter in snapshot.Keyboard)
                {
                    Database.Add(new LetterSample(letter));
                }
                this.SaveDatabase();
            }
        }

        internal void SaveUnknownSnapshotLettersIntoDatabase(RecognitionResults recognitionResults)
        {
            lock (DatabaseLock)
            {
                foreach (RecognizedLetter letter in recognitionResults.Keyboard.Where(rl => rl.Kind == RecognitionLetterKind.UnknownLetter))
                {
                    Database.Add(new LetterSample(letter.SnapshotLetter));
                }
                this.SaveDatabase();
            }
        }

        public void SaveDatabase()
        {
            lock (DatabaseLock)
            {
                Database.FirstTimeLaunch = false;
                Databases.Save(Database, Filename);
            }
        }

        internal void Remove(LetterSample editingLetter)
        {
            lock (DatabaseLock)
            {
                this.Database.Remove(editingLetter);
                SaveDatabase();
            }
        }

        internal void SwitchDatabase(string dbname)
        {
            Filename = dbname;
            try
            {
                Database = Databases.Load(dbname);
            }
            catch (Exception ex)
            {
                Database = new Database(true);
            }
        }

        internal void AddSingle(SnapshotLetter letter)
        {
            this.AddSingle(new LetterSample(letter));
        }
        internal void AddSingle(LetterSample letter)
        {
            lock (DatabaseLock)
            {
                Database.Add(letter);
                SaveDatabase();
            }
        }
    }
}
