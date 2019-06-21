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
        public Command LoadItemsCommand { get; set; }

        public SettingsViewModel()
        {
            grouped = new ObservableCollection<GroupedVeggieModel>();

            var veggieGroup = new GroupedVeggieModel() { LongName = "Settings", ShortName = "f" };
            var fruitGroup = new GroupedVeggieModel() { LongName = "Version", ShortName = "f" };
            veggieGroup.Add(new VeggieModel() { Name = "Check notes from", IsReallyAVeggie = true, Comment = sender });
            veggieGroup.Add(new VeggieModel() { Name = "Check notes to", IsReallyAVeggie = false, Comment = receiver });
            veggieGroup.Add(new VeggieModel() { Name = "Check notes date", IsReallyAVeggie = true, Comment = "Today" });
            veggieGroup.Add(new VeggieModel() { Name = "Add family members", IsReallyAVeggie = true });
            veggieGroup.Add(new VeggieModel() { Name = "Logout", IsReallyAVeggie = true });
            fruitGroup.Add(new VeggieModel() { Name = "Version", IsReallyAVeggie = false, Comment = "1.0" });
            grouped.Add(veggieGroup);
            grouped.Add(fruitGroup);

            MessagingCenter.Subscribe<FamilyMembers, String>(this, "Sender", (obj, Result) =>
            {
                sender = Result as String;
                //update the comment, find the item and update
                grouped[0].LongName = Result;
                //grouped.Add(new GroupedVeggieModel() { LongName = "Version", ShortName = "f" });
                Debug.WriteLine("**************************" + grouped[0].LongName);
            });
            MessagingCenter.Subscribe<FamilyMembers, String>(this, "Receiver", (obj, Result) =>
            {
                receiver = Result as String;
                veggieGroup[1].Comment = receiver;

            });
        }

    }
}
