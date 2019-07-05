using FamilyNoteApp.Models.dataStructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyNoteApp.Models.restService
{
    public class ServiceUtil
    {

        RestService restService;

        public ServiceUtil(RestService service)
        {
            restService = service;
        }

        public async Task<TokenSessionRestResult> Login(UserPostBody userPostBody)
        {
            return await restService.Login(userPostBody);
        }

        public async Task<BaseResult> Logout()
        {
            return await restService.Logout();
        }

        public async Task<TokenSessionRestResult> Register(UserPostBody userPostBody)
        {
            return await restService.Register(userPostBody);
        }

        public async Task<BaseResult> SearchNote(string keywords)
        {
            return await restService.SearchNote(keywords);
        }
        public async Task<BaseResult> FilterNote(string sender, string receiver, string date)
        {
            return await restService.FilterNote(sender, receiver, date);
        }
        public async Task<BaseResult> SubmitNote(NoteItem note)
        {
            return await restService.SubmitNote(note);
        }

        public async Task<BaseResult> AddFamilyMember(PostFamilyMembers postFamilyMembers)
        {
            return await restService.AddFamilyMembers(postFamilyMembers);
        }
        public async Task<BaseResult> GetFamilyMembers()
        {
            return await restService.GetFamilyMembers();
        }
    }
}
