using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FamilyNoteApp.ViewModels
{
    public class CustomCell : ViewCell
    {
        public CustomCell()
        {
            //instantiate each of our views
           
            StackLayout cellWrapper = new StackLayout();
            StackLayout horizontalLayout = new StackLayout();
            Label section = new Label();
            Label title = new Label();
            Label subtitle = new Label();

            //set bindings
            title.SetBinding(Label.TextProperty, "Text");
            subtitle.SetBinding(Label.TextProperty, "Description");
            section.SetBinding(Label.TextProperty, "Section");

            //Set properties for desired design
            cellWrapper.BackgroundColor = Color.FromHex("#eee");
  
            if (section == null)
            {
                section.HeightRequest = 0;
                
            } 

            title.TextColor = Color.FromHex("#f35e20");
            subtitle.TextColor = Color.FromHex("503026");

            //add views to the view hierarchy
            
            horizontalLayout.Children.Add(section);
            horizontalLayout.Children.Add(title);
            horizontalLayout.Children.Add(subtitle);
            cellWrapper.Children.Add(horizontalLayout);
            View = cellWrapper;
        }
    }
}
