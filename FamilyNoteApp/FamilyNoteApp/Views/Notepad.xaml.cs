using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyNoteApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyNoteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notepad : ContentPage
    {
        NotepadViewModel viewModel;

        public Notepad()
        {
            InitializeComponent();

            BindingContext = viewModel = new NotepadViewModel();
        }

        void Submit_Clicked(object sender, EventArgs e)
        {

            //Application.Current.MainPage = new AppShell();
        }
    }
}