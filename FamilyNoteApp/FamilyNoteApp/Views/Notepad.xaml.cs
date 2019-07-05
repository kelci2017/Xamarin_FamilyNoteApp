using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyNoteApp.Models.dataStructure;
using FamilyNoteApp.Models.restService;
using FamilyNoteApp.ViewModels;
using Xamarin.Essentials;
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
            observeViewModel();
        }

        void Submit_Clicked(object sender, EventArgs e)
        {
            submiteNote();
        }

        private void observeViewModel()
        {
            MessagingCenter.Subscribe<RestService, BaseResult>(this, "SubmiteNote", async (obj, Result) =>
            {

                var result = Result as BaseResult;
                if (result != null && result.resultCode == Constants.result_success)
                {
                    if (from.Text.Equals("")) from.Text = "All";
                    if (receiver.Text.Equals("")) receiver.Text = "All";
                    Debug.WriteLine("From " + from.Text + "App sender: " + App.Sender + " To: " + receiver.Text + "App receiver: " + App.Receiver + " Date: " + App.NoteDate);
                    if ((from.Text.Equals(App.Sender) || App.Sender.Equals("All")) && (receiver.Text.Equals(App.Receiver) || App.Receiver.Equals("All")) && App.NoteDate.Equals(DateTime.Now.ToString().Substring(0, 10)))
                    {
                        await App.serviceUtil.FilterNote(App.Sender, App.Receiver, DateTime.Now.ToString().Substring(0, 10));
                    }
                    from.Text = "";
                    receiver.Text = "";
                    noteBody.Text = "";
                }
                else if (result.resultCode == Constants.result_token_expired)
                {
                    Preferences.Set("token", null);
                    submiteNote();
                }
                else if (result.resultCode == Constants.result_timeout)
                {
                    Preferences.Set("token", null);
                    Preferences.Set("sessionid", null);
                    Application.Current.MainPage = new Login();
                }
                else
                {
                    //show error dialog
                    await DisplayAlert("Submit failed!", Result.resultDesc.ToString(), "OK");
                }
            });
        }

        private async void submiteNote()
        {
            NoteItem note = new NoteItem { fromWhom = from.Text, toWhom = receiver.Text, created = DateTime.Now.ToString().Substring(0,10), noteBody = noteBody.Text, userID = Preferences.Get("userID", null) };

            await App.serviceUtil.SubmitNote(note);
        }
    }
}