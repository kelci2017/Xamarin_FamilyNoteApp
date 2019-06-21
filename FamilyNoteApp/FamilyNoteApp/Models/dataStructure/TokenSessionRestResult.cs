using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyNoteApp.Models.dataStructure
{
    public class TokenSessionRestResult : BaseResult
    {
        public string token { get; set; }

        public string sessionID { get; set; }

        public string userID { get; set; }
    }
}
