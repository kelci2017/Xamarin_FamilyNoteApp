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
        public ObservableCollection<GroupedVeggieModel> grouped { get; set; }
        private String sender = "All";
        private String receiver = "All";
        private String noteDate = "Today";
        public Command LoadItemsCommand { get; set; }

        public SettingsViewModel()
        {
            Title = "FamilyNoteApp";
            grouped = new ObservableCollection<GroupedVeggieModel>();
            SetList();

            MessagingCenter.Subscribe<FamilyMembers, String>(this, "Sender", async (obj, Result) =>
            {
                sender = Result as String;
                App.Sender = sender;
                grouped.Clear();
                SetList();
                await App.serviceUtil.FilterNote(sender, receiver, noteDate);
            });
            MessagingCenter.Subscribe<FamilyMembers, String>(this, "Receiver", async (obj, Result) =>
            {
                receiver = Result as String;
                App.Receiver = receiver;
                grouped.Clear();
                SetList();
                await App.serviceUtil.FilterNote(sender, receiver, noteDate);
            });
            MessagingCenter.Subscribe<NoteDate, String>(this, "NoteDate", async (obj, Result) =>
            {
                noteDate = Result as String;
                App.NoteDate = noteDate;
                grouped.Clear();
                SetList();
                await App.serviceUtil.FilterNote(sender, receiver, noteDate);
            });
        }
        private void SetList()
        {
            var veggieGroup = new GroupedVeggieModel() { LongName = "Settings", ShortName = "f" };
            var fruitGroup = new GroupedVeggieModel() { LongName = "Version", ShortName = "f" };
            veggieGroup.Add(new VeggieModel() { Name = "Check notes from", IsReallyAVeggie = true, Comment = sender });
            veggieGroup.Add(new VeggieModel() { Name = "Check notes to", IsReallyAVeggie = false, Comment = receiver });
            veggieGroup.Add(new VeggieModel() { Name = "Check notes date", IsReallyAVeggie = true, Comment = noteDate });
            veggieGroup.Add(new VeggieModel() { Name = "Add family members", IsReallyAVeggie = true });
            veggieGroup.Add(new VeggieModel() { Name = "Logout", IsReallyAVeggie = true });
            fruitGroup.Add(new VeggieModel() { Name = "Version", IsReallyAVeggie = false, Comment = "1.0" });
            grouped.Add(veggieGroup);
            grouped.Add(fruitGroup);
        }
    }
}
