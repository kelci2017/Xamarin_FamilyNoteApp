using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyNoteApp.Models.dataStructure
{
    public class PostFamilyMembers
    {
        public string userID { get; set; }

        public ObservableCollection<string> familyMembers { get; set; }
    }
}
