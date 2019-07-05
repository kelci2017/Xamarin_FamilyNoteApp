using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FamilyNoteApp.Models;
using FamilyNoteApp.Views;
using FamilyNoteApp.ViewModels;
using FamilyNoteApp.Models.restService;
using FamilyNoteApp.Models.dataStructure;
using Xamarin.Essentials;
using System.Collections.ObjectModel;

namespace FamilyNoteApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class Noteboard : ContentPage
    {
        NoteboardViewModel viewModel;
        public ObservableCollection<NewNote> Items { get; set; }
        public Noteboard()
        {
            InitializeComponent();

            BindingContext = viewModel = new NoteboardViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Items.Count == 0)
            //    viewModel.LoadItemsCommand.Execute(null);
        }
        
    }
}