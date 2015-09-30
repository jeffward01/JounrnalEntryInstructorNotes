using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalNotes
{
    //Class
    class Journal
    {
        //Constructor
        public Journal(string title)
        {
            Title = title;

            Entries = new ObservableCollection<JournalEntry>();

          //  var entry = new JournalEntry();
        }

        //Properties
        //Get Set makes it accessable from the outside
        public string Title { get; set; }
        public ObservableCollection<JournalEntry> Entries { get; set; }
    }
}
