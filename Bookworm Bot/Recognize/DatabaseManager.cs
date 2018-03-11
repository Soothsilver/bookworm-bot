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
        public Database Database { get; set; }

        internal void SaveAllSnapshotLettersIntoDatabase(Snapshot snapshot)
        {
            foreach(SnapshotLetter letter in snapshot.Keyboard)
            {
                Database.Add(new LetterSample(letter));
            }
            this.SaveDatabase();
        }

        internal void SaveUnknownSnapshotLettersIntoDatabase(RecognitionResults recognitionResults)
        {
            foreach (RecognizedLetter letter in recognitionResults.Keyboard.Where(rl => rl.Kind == RecognitionLetterKind.UnknownLetter))
            {
                Database.Add(new LetterSample(letter.SnapshotLetter));
            }
            this.SaveDatabase();
        }

        public void SaveDatabase()
        {
            Databases.Save(Database);            
        }

        internal void Remove(LetterSample editingLetter)
        {
            this.Database.Remove(editingLetter);
            SaveDatabase();
        }
    }
}
