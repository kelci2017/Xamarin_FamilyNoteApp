using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyNoteApp.Models;

namespace FamilyNoteApp.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", NoteBody="This is an item description." , FromWhom = "Arwin", ToWhom = "Kelci", NoteDate="2019-06-03"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", NoteBody="This is an item description." , FromWhom = "Kelci", ToWhom = "Emma", NoteDate="2019-06-13"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", NoteBody="This is an item description." , FromWhom = "Alisa", ToWhom = "Arwin", NoteDate="2019-06-11"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", NoteBody="This is an item description." , FromWhom = "Henry", ToWhom = "Kelci", NoteDate="2019-06-15"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", NoteBody="This is an item description." , FromWhom = "Emma", ToWhom = "Henry", NoteDate="2019-06-23"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", NoteBody="This is an item description." , FromWhom = "Arwin", ToWhom = "Alisa", NoteDate="2019-06-28"}
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}