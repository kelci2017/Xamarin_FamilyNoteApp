using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FamilyNoteApp.Services;
using FamilyNoteApp.Views;
using FamilyNoteApp.Models;
using FamilyNoteApp.Models.restService;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace FamilyNoteApp
{
    public partial class App : Application
    {
        public static ServiceUtil serviceUtil { get; private set; }
        public App()
        {
            InitializeComponent();

            serviceUtil = new ServiceUtil(new RestService());

            DependencyService.Register<MockDataStore>();
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
            return (T)Xamarin.Forms.Application.Current.Properties[key];
        }
    }
}
