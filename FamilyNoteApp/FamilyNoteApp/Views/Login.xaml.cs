
using FamilyNoteApp.Models.dataStructure;
using FamilyNoteApp.Models.restService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyNoteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            observeViewModel();
        }

        async void Login_Clicked(object sender, EventArgs e)
        {
            //check email format and password format before call the webservice
            if (email.Text == null || email.Text == "" )
            {
                _ = DisplayAlert("Empty email!", "Please enter your email", "OK");
                return;
            }
            if (!IsValidEmail(email.Text))
            {
                _ = DisplayAlert("Wrong email format!", "Please enter a valid email address.", "OK");
                return;
            }
            if (password.Text == null || password.Text == "" || password.Text.Length == 0)
            {
                _ = DisplayAlert("Empty password!", "Please enter your password.", "OK");
                return;
            }
            if ( password.Text.Length <8)
            {
                _ = DisplayAlert("Wrong email format!", "Please enter a valid email address.", "OK");
                return;
            }
            
            
            await App.serviceUtil.Login(new UserPostBody { email = email.Text, password = password.Text});
         
        }
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Register()));
        }

        private void observeViewModel()
        {
            MessagingCenter.Subscribe<RestService, TokenSessionRestResult>(this, "Login", (obj, Result) =>
            {

                var result = Result as TokenSessionRestResult;
                if (result != null && result.resultCode == Constants.result_success)
                {
                    //save the userid and token in the local preferences
                    Preferences.Set("userID", Result.userID);
                    Preferences.Set("sessionid", Result.sessionID);
                    Preferences.Set("token", Result.token);
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    //show error dialog
                    DisplayAlert("Login failed!", Result.resultDesc.ToString(), "OK");
                }
            });
        }
    }
}