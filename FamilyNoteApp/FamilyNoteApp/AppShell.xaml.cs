using FamilyNoteApp.Models.restService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FamilyNoteApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            if (App.LoadApplicationProperty<ObservableCollection<string>>("familyMembers").Count == 0)
            {
                getFamilyMmembers();
            }
        }

        async void getFamilyMmembers()
        {
           await App.serviceUtil.GetFamilyMembers();
        }
    }
}
