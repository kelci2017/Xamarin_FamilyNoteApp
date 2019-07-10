using FamilyNoteApp.Models.dataStructure;
using FamilyNoteApp.Models.restService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyNoteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFamilyMembers : ContentPage
    {
        ObservableCollection<string> memberList;
        public AddFamilyMembers()
        {
            InitializeComponent();
            memberList = new ObservableCollection<string>();
            
            observeViewModel();
        }

         async void Add_Clicked(object sender, EventArgs e)
        {
            if (App.LoadApplicationProperty<ObservableCollection<string>>("familyMembers") != null)
            
            {
                memberList = App.LoadApplicationProperty<ObservableCollection<string>>("familyMembers");
            }
            addFamilyMembers();
            //if (memberList.Count == App.LoadApplicationProperty<ObservableCollection<string>>("familyMembers").Count) return;
            PostFamilyMembers postList = new PostFamilyMembers() { userID = Preferences.Get("userID", null), familyMembers = memberList };
            await App.serviceUtil.AddFamilyMember(postList);
        }
        private void addFamilyMembers()
        {
            addFamilyMember(name1.Text);
            addFamilyMember(name2.Text);
            addFamilyMember(name3.Text);
            addFamilyMember(name4.Text);
            addFamilyMember(name5.Text);
            addFamilyMember(name6.Text);
            addFamilyMember(name7.Text);
            addFamilyMember(name8.Text);
            addFamilyMember(name9.Text);
        }
        private void addFamilyMember(string name)
        {
            if (name == null) return;
            if (name.Trim() == "") return;
            if (memberList.Contains(name)) return;
            memberList.Add(name);
            
        }

        private void observeViewModel()
        {
            MessagingCenter.Subscribe<RestService, BaseResult>(this, "AddFamilyMembers", async(obj, Result) =>
            {

                var result = Result as BaseResult;
                if (result != null && result.resultCode == Constants.result_success)
                {
                    _ = await Navigation.PopModalAsync();
                }
                else if (result.resultCode == Constants.result_token_expired)
                {
                    Preferences.Set("token", null);
                    addFamilyMembers();
                } else if (result.resultCode == Constants.result_timeout)
                {
                    Preferences.Set("token", null);
                    Preferences.Set("sessionid", null);
                    Application.Current.MainPage = new Login();
                }
                else
                {
                    //show error dialog
                    await DisplayAlert("Add failed!", Result.resultDesc.ToString(), "OK");
                }
            });
        }
    }
}