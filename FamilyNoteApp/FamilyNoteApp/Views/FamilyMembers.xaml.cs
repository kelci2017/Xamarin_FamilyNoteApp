using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamilyNoteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FamilyMembers : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        private String from;

        public FamilyMembers(String from)
        {
            InitializeComponent();
            this.from = from;
            Items = new ObservableCollection<string>
            {
                "Kelci",
                "Arwin",
                "Alisa",
                "Emma",
                "Henry"
            };

            MyListView.ItemsSource = Items;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as String;
            if (item == null)
                return;
            if (from.Equals("sender") ) {
                MessagingCenter.Send(this, "Sender", item);
                await Navigation.PopModalAsync();
            } else
            {
                MessagingCenter.Send(this, "Receiver", item);
                await Navigation.PopModalAsync();
            }
            // Manually deselect item.
            MyListView.SelectedItem = null;
        }
    }
}
