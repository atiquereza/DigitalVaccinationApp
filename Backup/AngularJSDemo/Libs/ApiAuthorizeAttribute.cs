using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Http
{
    public class ApiAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        private string _responseReason = "";
        public bool ByPassAuthorization { get; set; }

    }
}

////http://www.asp.net/web-api/overview/security/authentication-filters