using System;

namespace FamilyNoteApp.Models
{
    public class Item
    {
        public string Id { get; set; }
        
        public string Text { get; set; }
        public string Description { get; set; }

        public bool IsSection { get; set; }

        public string FromWhom { get; set; }

        public string ToWhom { get; set; }
        public string NoteBody { get; set; }

        public string NoteDate { get; set; }
    }
}