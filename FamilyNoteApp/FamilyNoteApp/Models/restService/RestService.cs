using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using FamilyNoteApp.Models.dataStructure;
using Newtonsoft.Json;
using System.Diagnostics;
using Xamarin.Forms;

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

        
    }
}
