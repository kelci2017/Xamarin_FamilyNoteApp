using FamilyNoteApp.Models;
using FamilyNoteApp.Models.dataStructure;
using FamilyNoteApp.Models.restService;
using FamilyNoteApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FamilyNoteApp.Views
{
    public partial class Settings : ContentPage
    {

        SettingsViewModel viewModel;

        public Settings()
        {
            InitializeComponent();
            BindingContext = viewModel = new SettingsViewModel();
            ObserveViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as SettingItem;
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
                await Navigation.PushModalAsync(new NavigationPage(new NoteDate()));
            }
            else if (item.Name.Equals("Logout"))
            {
                await App.serviceUtil.Logout();
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
        private void ObserveViewModel()
        {
            MessagingCenter.Subscribe<RestService, BaseResult>(this, "Logout", (obj, Result) =>
            {
                var result = Result as BaseResult;
                if (result != null && (result.resultCode == Constants.result_success || result.resultCode == Constants.result_timeout))
                {
                    Preferences.Set("token", null);
                    Preferences.Set("sessionid", null);
                    Application.Current.MainPage = new Login();
                }
                else if (result.resultCode == Constants.result_token_expired)
                {
                    Preferences.Set("token", null);
                    _ = App.serviceUtil.Logout();
                }
                else
                {
                    //show error dialog
                    DisplayAlert("Logout failed!", Result.resultDesc.ToString(), "OK");
                }
            });
        }
    }
}