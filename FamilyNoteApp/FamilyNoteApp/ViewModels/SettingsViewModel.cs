using FamilyNoteApp.Models;
using FamilyNoteApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace FamilyNoteApp.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        public ObservableCollection<GroupedSettingItem> grouped { get; set; }
        private String sender = "All";
        private String receiver = "All";
        private String noteDate = "Today";
        public Command LoadItemsCommand { get; set; }

        public SettingsViewModel()
        {
            Title = "FamilyNoteApp";
            grouped = new ObservableCollection<GroupedSettingItem>();
            SetList();

            MessagingCenter.Subscribe<FamilyMembers, String>(this, "Sender", (obj, Result) =>
            {
                sender = Result as String;
                App.Sender = sender;
                grouped.Clear();
                SetList();
                FilterNotes();

            });
            MessagingCenter.Subscribe<FamilyMembers, String>(this, "Receiver", (obj, Result) =>
            {
                receiver = Result as String;
                App.Receiver = receiver;
                grouped.Clear();
                SetList();
                FilterNotes();
            });
            MessagingCenter.Subscribe<NoteDate, String>(this, "NoteDate", (obj, Result) =>
            {

                noteDate = Result as String;
                Debug.WriteLine("&&&&&&&&&&&&&&&" + noteDate);
                App.NoteDate = noteDate;
                grouped.Clear();
                SetList();
                FilterNotes();
            });
        }
        private void SetList()
        {
            var settingGroup = new GroupedSettingItem() { LongName = "Settings", ShortName = "" };
            var versionGroup = new GroupedSettingItem() { LongName = "Version", ShortName = "" };
            settingGroup.Add(new SettingItem() { Name = "Check notes from", Comment = sender });
            settingGroup.Add(new SettingItem() { Name = "Check notes to", Comment = receiver });
            settingGroup.Add(new SettingItem() { Name = "Check notes date", Comment = noteDate });
            settingGroup.Add(new SettingItem() { Name = "Add family members" });
            settingGroup.Add(new SettingItem() { Name = "Logout" });
            versionGroup.Add(new SettingItem() { Name = "Version", Comment = "1.0" });
            grouped.Add(settingGroup);
            grouped.Add(versionGroup);
        }

        private async void FilterNotes()
        {
            if (noteDate.Equals("Today"))
            {
                await App.serviceUtil.FilterNote(sender, receiver, DateTime.Now.ToString("MM/dd/yyyy"));
            }
            else
            {
                await App.serviceUtil.FilterNote(sender, receiver, noteDate);
            }
        }
    }
}
