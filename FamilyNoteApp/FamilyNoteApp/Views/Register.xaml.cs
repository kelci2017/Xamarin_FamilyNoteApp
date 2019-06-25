using FamilyNoteApp.Models.dataStructure;
using FamilyNoteApp.Models.restService;
using System;
using System.Collections.Generic;
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
    public partial class Register : ContentPage
    {
        public Register()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        
        async void Register_Clicked(object sender, EventArgs e)
        {
            //check email format and password format before call the webservice
            if (register_email.Text == null || register_email.Text == "")
            {
                _ = DisplayAlert("Empty email!", "Please enter your email", "OK");
                return;
            }
            if (!IsValidEmail(register_email.Text))
            {
                _ = DisplayAlert("Wrong email format!", "Please enter a valid email address.", "OK");
                return;
            }
            if (register_password.Text == null || register_password.Text == "" || register_password.Text.Length == 0)
            {
                _ = DisplayAlert("Empty password!", "Please enter your password.", "OK");
                return;
            }
            if (register_password.Text.Length < 8)
            {
                _ = DisplayAlert("Wrong email format!", "Please enter a valid email address.", "OK");
                return;
            }
            if (register_password.Text.Equals(reenter_password.Text))
            {
                _ = DisplayAlert("Password is not the same!", "Please re-enter your password.", "OK");
                return;
            }

            const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";
            if (!Regex.IsMatch(register_password.Text, passwordRegex))
            {
                _ = DisplayAlert("Wrong email format!", "Please enter a pass", "OK");
            }

            await App.serviceUtil.Login(new UserPostBody { email = register_email.Text, password = register_password.Text });
            //await Navigation.PushModalAscync(new AppShell());
            MessagingCenter.Subscribe<RestService, TokenSessionRestResult>(this, "Register", (obj, Result) =>
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
                    DisplayAlert("Register failed!", Result.resultDesc.ToString(), "OK");
                }
            });


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
    }
}