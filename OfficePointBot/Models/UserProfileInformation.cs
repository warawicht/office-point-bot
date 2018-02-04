using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficePointBot
{ 
    public class UserProfileInformation
    {
        public string id { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string link { get; set; }
        public object gender { get; set; }
        public string locale { get; set; }
        public string updated_time { get; set; }
    }

}