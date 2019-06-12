using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using FamilyNoteApp.Models;
using System.Diagnostics;
using FamilyNoteApp.Views;
using System.Threading.Tasks;

namespace FamilyNoteApp.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> SettingItems { get; set; }
        public ObservableCollection<SettingsItems> SettingsItems { get; set; }
        public ObservableCollection<Section> SettingSections { get; set; }
        public Command LoadItemsCommand { get; set; }

        List<Item> settingitems;
        List<Section> settingsections;

        public SettingsViewModel()
        {
            

            Title = "FamilyNoteApp";
            SettingItems = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                SettingItems.Clear();
                settingitems = new List<Item>();
                var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = null, Description=null},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Check notes from", Description="All"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Check notes to", Description="All"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Check notes date", Description="Today" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Add family members", Description=null },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Logout", Description=null },
                new Item { Id = Guid.NewGuid().ToString(), Text = null, Description=null },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Version", Description="1.0"},
            };

                foreach (var item in mockItems)
                {
                    settingitems.Add(item);
                }
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in settingitems)
                {
                    SettingItems.Add(item);
                }
                SettingsItems.Add(new SettingsItems { SettingSection = "Settings", SettingContent = settingitems });
                SettingsItems.Add(new SettingsItems { SettingSection = "About", SettingContent = settingitems });
                SettingSections.Add(new Section { Title = "Settings" });
                SettingSections.Add(new Section { Title = "About" });
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
    }
}
