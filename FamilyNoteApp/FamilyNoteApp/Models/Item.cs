using System;

namespace FamilyNoteApp.Models
{
    public class Item
    {
        public string Id { get; set; }
        
        public string Text { get; set; }
        public string Description { get; set; }

        public bool IsSection { get; set; }
    }
}