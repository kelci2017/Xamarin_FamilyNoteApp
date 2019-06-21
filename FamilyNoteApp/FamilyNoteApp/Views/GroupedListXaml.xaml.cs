using FamilyNoteApp.Models;
using FamilyNoteApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FamilyNoteApp.Views
{
    public partial class GroupedListXaml : ContentPage
    {

        SettingsViewModel viewModel;

        public GroupedListXaml()
        {
            InitializeComponent();
            BindingContext = viewModel = new SettingsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as VeggieModel;
            if (item == null)
                return;

            if (item.Name.Equals("Check notes from"))
            {
                await Navigation.PushModalAsync(new NavigationPage(new FamilyMembers("sender")));
            }
            else if (item.Name.Equals("Check notes to"))
            {
                await Navigation.PushModalAsync(new NavigationPage(new FamilyMembers("receiver")));
            }
            else if (item.Name.Equals("Add family members"))
            {
                await Navigation.PushModalAsync(new NavigationPage(new AddFamilyMembers()));
            }
            else if (item.Name.Equals("Check notes date"))
            {
                await Navigation.PushModalAsync(new NavigationPage(new AddFamilyMembers()));
            }
            else if (item.Name.Equals("Logout"))
            {

            }

            // Manually deselect item.
            lstView.SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.grouped.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}