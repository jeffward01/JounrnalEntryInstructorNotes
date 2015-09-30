using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalNotes
{
    //Class
    class JournalEntry
    {
        //Properties
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string Title { get; set; }
        public string Entry { get; set; }

    }

}
