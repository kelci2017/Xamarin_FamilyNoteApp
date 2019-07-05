using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FamilyNoteApp.Views;
using FamilyNoteApp.Models;
using FamilyNoteApp.Models.restService;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FamilyNoteApp
{
    public partial class App : Application
    {
        public static ServiceUtil serviceUtil { get; private set; }
        public static string Sender = "All";
        public static string Receiver = "All";
        public static string NoteDate = DateTime.Now.ToString().Substring(0, 10);

        public App()
        {
            InitializeComponent();

            serviceUtil = new ServiceUtil(new RestService());
            intializeFamilyList();
            if (Preferences.Get("token", null) != null)
            {
                MainPage = new AppShell();
            } else
            {
                MainPage = new NavigationPage(new Login());
            }          
        }   

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public static async Task SaveApplicationProperty<T>(string key, T value)
        {
            Xamarin.Forms.Application.Current.Properties[key] = value;
            await Xamarin.Forms.Application.Current.SavePropertiesAsync();
        }

        public static T LoadApplicationProperty<T>(string key)
        {
            if (key == null) return default;
            return (T)Xamarin.Forms.Application.Current.Properties[key];
        }
        private async void intializeFamilyList()
        {
            ObservableCollection<string> familyMemberList = new ObservableCollection<string>();
            await SaveApplicationProperty("familyMembers", familyMemberList);
        }
    }
}
