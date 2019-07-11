using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyNoteApp.Models
{
    public class SettingItem
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public SettingItem()
        {
        }
    }

    public class GroupedSettingItem : ObservableCollection<SettingItem>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}
