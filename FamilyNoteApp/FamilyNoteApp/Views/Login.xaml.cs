
using FamilyNoteApp.Models.dataStructure;
using FamilyNoteApp.Models.restService;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        async void Login_Clicked(object sender, EventArgs e)
        {
            //check email format and password format before call the webservice
            if (email.Text == "" || !email.Text.Contains("@xyz.com"))
            {
                _ = DisplayAlert("Wrong email format!", "Please enter a valid email address.", "OK");
            }
            if (password.Text.Length <8)
            {
                _ = DisplayAlert("Wrong email format!", "Please enter a valid email address.", "OK");
            }
            if (password.Text.Length == 0)
            {
                _ = DisplayAlert("Empty password!", "Please enter your password.", "OK");
            }
            //const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";
            //if (!Regex.IsMatch(password.Text, passwordRegex))
            //{
            //    _ = DisplayAlert("Wrong email format!", "Please enter a pass", "OK");
            //}
            await App.serviceUtil.Login(new UserPostBody { email = email.Text, password = password.Text});
            //await Navigation.PushModalAscync(new AppShell());
            MessagingCenter.Subscribe<RestService, TokenSessionRestResult>(this, "Login", (obj, Result) =>
            {
                var result = Result as TokenSessionRestResult;
                if (result != null && result.resultCode == 0)
                {
                    //save the userid and token in the local preferences
                    Preferences.Set("userID", Result.userID);
                    Preferences.Set("token", Result.token);
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    //show error dialog
                    DisplayAlert("Login failed!", Result.resultDesc, "OK");
                }
            });

            
        }

        async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Register()));
        }
    }
}