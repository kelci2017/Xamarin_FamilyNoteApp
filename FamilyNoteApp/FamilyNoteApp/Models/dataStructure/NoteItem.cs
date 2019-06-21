using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyNoteApp.Models.dataStructure
{
    class NoteItem
    {
        public string fromWhom { get; set; }

        public string toWhom { get; set; }
        public string noteBody { get; set; }

        public string created { get; set; }

        public string userID { get; set; }
    }
}
