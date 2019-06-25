using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;

namespace FamilyNoteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteDate : ContentPage
    {
        public NoteDate()
        {
            InitializeComponent();
        }
        void DateClicked (object sender, EventArgs e)
        {
           
            var dateSelect = calendar.SelectedDate;
            Debug.WriteLine("*************************" + dateSelect.ToString());
        }
}
}