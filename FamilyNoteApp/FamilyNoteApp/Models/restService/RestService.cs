using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using FamilyNoteApp.Models.dataStructure;
using Newtonsoft.Json;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;

namespace FamilyNoteApp.Models.restService
{
    public class RestService
    {

        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }
        public async Task<TokenSessionRestResult> Login(UserPostBody userPostBody)
        {

            TokenSessionRestResult Result = new TokenSessionRestResult();

            var uri = new Uri(Constants.login);
            var json = JsonConvert.SerializeObject(userPostBody);
            var postbody = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync(uri, postbody);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<TokenSessionRestResult>(content);
                    MessagingCenter.Send(this, "Login", Result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Result;
        }

        public async Task<TokenSessionRestResult> Register(UserPostBody userPostBody)
        {

            TokenSessionRestResult Result = new TokenSessionRestResult();

            var uri = new Uri(Constants.register);
            var json = JsonConvert.SerializeObject(userPostBody);
            var postbody = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync(uri, postbody);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<TokenSessionRestResult>(content);
                    MessagingCenter.Send(this, "Register", Result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Result;
        }

        public async Task<BaseResult> Logout()
        {

            BaseResult Result = new BaseResult();
            if (Preferences.Get("token", null) == null)
            {
                GetToken();
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("token", null));

            var uri = new Uri(string.Format(Constants.logout, Preferences.Get("sessionid", null)));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<BaseResult>(content);
                    MessagingCenter.Send(this, "Logout", Result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Result;
        }

        public async Task<BaseResult> GetFamilyMembers()
        {

            BaseResult Result = new BaseResult();
            if (Preferences.Get("token", null) == null)
            {
                GetToken();
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("token", null));

            var uri = new Uri(string.Format(Constants.add_family_members, Preferences.Get("sessionid", null)));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<BaseResult>(content);
                    ObservableCollection<string> familyMemberList = Result.resultDesc as ObservableCollection<string>;
                    await App.SaveApplicationProperty("familyMembers", familyMemberList);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Result;
        }
        public async Task<BaseResult> AddFamilyMembers(PostFamilyMembers postFamilyMembers)
        {

            BaseResult Result = new BaseResult();
            if (Preferences.Get("token", null) == null)
            {
                GetToken();
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("token", null));

            var uri = new Uri(string.Format(Constants.add_family_members, Preferences.Get("sessionid", null)));
            var json = JsonConvert.SerializeObject(postFamilyMembers);
            var postbody = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync(uri, postbody);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<BaseResult>(content);
                    MessagingCenter.Send(this, "AddFamilyMembers", Result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Result;
        }
        public async Task<BaseResult> SubmitNote(NoteItem noteItem)
        {

            BaseResult Result = new BaseResult();
            if (Preferences.Get("token", null) == null)
            {
                GetToken();
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("token", null));

            var uri = new Uri(string.Format(Constants.submit_note, Preferences.Get("sessionid", null), "app_unique_id"));
            var json = JsonConvert.SerializeObject(noteItem);
            var postbody = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync(uri, postbody);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<BaseResult>(content);
                    MessagingCenter.Send(this, "SubmitNote", Result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Result;
        }
        public async Task<BaseResult> FilterNote(string sender, string receiver, string date)
        {

            BaseResult Result = new BaseResult();
            if (Preferences.Get("token", null) == null)
            {
                GetToken();
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("token", null));

            var uri = new Uri(string.Format(Constants.note_filter, Preferences.Get("sessionid", null), sender, receiver, date));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<BaseResult>(content);
                    ObservableCollection<NewNote> filteredNoteList = Result.resultDesc as ObservableCollection<NewNote>;
                    await App.SaveApplicationProperty("filteredNote", filteredNoteList);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Result;
        }
        public async Task<BaseResult> SearchNote(string keywords)
        {

            BaseResult Result = new BaseResult();
            if (Preferences.Get("token", null) == null)
            {
                GetToken();
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("token", null));

            var uri = new Uri(string.Format(Constants.note_search, keywords, Preferences.Get("sessionid", null)));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<BaseResult>(content);
                    ObservableCollection<NewNote> searchedNoteList = Result.resultDesc as ObservableCollection<NewNote>;
                    await App.SaveApplicationProperty("searchedNote", searchedNoteList);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Result;
        }
        private async void GetToken()
        {
            TokenSessionRestResult Result = new TokenSessionRestResult();
            var uri = new Uri(string.Format(Constants.get_token, Preferences.Get("sessionid", null)));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<TokenSessionRestResult>(content);
                    Preferences.Set("token", Result.token);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            
        }
    }
}
