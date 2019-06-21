using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        void Register_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAscync(new AppShell());

            Application.Current.MainPage = new AppShell();
        }
    }
}