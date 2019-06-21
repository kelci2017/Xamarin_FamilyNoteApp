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

        //public async Task<BaseResult> Logout()
        //{
        //    return null;
        //}

        //public async Task<BaseResult> Register()
        //{
        //    return null;
        //}

        //public async Task<BaseResult> SearchNote()
        //{
        //    return null;
        //}

        //public async Task<BaseResult> SubmitNote()
        //{
        //    return null;
        //}

        //public async Task<BaseResult> AddFamilyMember()
        //{
        //    return null;
        //}
    }
}
