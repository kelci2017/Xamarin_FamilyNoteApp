using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FamilyNoteApp
{
    class Constants
    {

        public static string BaseAddress = "http://jacob.jandjzone.com:9110";
        //public static string BaseAddress = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";
        public static string TodoItemsUrl = BaseAddress + "/api/todoitems/{0}";

        public static string login = BaseAddress + "/auth/sign_in";
        public static string logout = BaseAddress + "/auth/sign_out?sessionid={0}";
        public static string register = BaseAddress + "/auth/register";
        public static string add_family_member = BaseAddress + "/auth/familyMembers?sessionid={0}";
        public static string submit_note = BaseAddress + "/notes/create?sessionid={0}&deviceid={1}";
        public static string note_filter = BaseAddress + "/notes/search?from={0}&to={1}&date={2}&sessionid={3}";
        public static string get_token = BaseAddress + "/auth/getToken?sessionid={0}";
        public static string get_family_members = BaseAddress + "/auth/familyMembers?sessionid={0}";
        public static string note_search = BaseAddress + "/notes/globalSearch/{0}?sessionid={1}";
        public static string notification_register = BaseAddress + "/auth/registerNotification?sessionid={0}";

    }
}
