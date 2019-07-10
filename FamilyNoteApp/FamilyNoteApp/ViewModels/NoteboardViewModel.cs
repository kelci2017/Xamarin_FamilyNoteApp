using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using FamilyNoteApp.Models;
using FamilyNoteApp.Views;
using FamilyNoteApp.Models.restService;
using FamilyNoteApp.Models.dataStructure;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;

namespace FamilyNoteApp.ViewModels
{
    public class NoteboardViewModel : BaseViewModel
    {
        //public ObservableCollection<NewNote> Items { get; set; }
        public ObservableCollection<NewNote> Notes { get; set; }
        ObservableCollection<NewNote> filteredNoteList { get; set; }
        public Command LoadItemsCommand { get; set; }

        public NoteboardViewModel()
        {
            Title = "FamilyNoteApp";
            FilterNote();
            //Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand());
            ObserveViewModel();
            Notes = new ObservableCollection<NewNote>();
            filteredNoteList = new ObservableCollection<NewNote>();
        }
        private async void FilterNote()
        {
            await App.serviceUtil.FilterNote("All", "All", DateTime.Now.ToString("MM/dd/yyyy"));
        }

        private void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //Items.Clear();
                Notes.Clear();
                foreach (var item in filteredNoteList)
                {
                    Notes.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private void ObserveViewModel()
        {
            
            MessagingCenter.Subscribe<RestService, JObject>(this, "FilteredNote", (obj, Result) =>
            {

                var result = Result as JObject;
                int resultCode = result["resultCode"].Value<int>();

                if (result != null && resultCode == Constants.result_success)
                {
                    var clientarray = result["resultDesc"].Value<JArray>();
                    filteredNoteList = clientarray.ToObject<ObservableCollection<NewNote>>();

                    //Notes = filteredNoteList;
                    Notes.Clear();
                    foreach (var item in filteredNoteList)
                    {
                        Notes.Add(item);
                        Debug.WriteLine("Note date is:" + item.created);
                    }
                    Debug.WriteLine("******************" + Notes.Count);
                }
                else if (resultCode == Constants.result_token_expired)
                {
                    Preferences.Set("token", null);

                }
                else if (resultCode == Constants.result_timeout)
                {

                    Preferences.Set("token", null);
                    Preferences.Set("sessionid", null);
                    Application.Current.MainPage = new Login();
                }
                else
                {
                    //show error dialog
                    //await DisplayAlert("Submit failed!", Result.resultDesc.ToString(), "OK");
                }
            });
           
        }
    }
}