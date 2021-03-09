using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace SwaggerAPI.ViewModel.AdminViewModel
{
    public class IIdentityViewModel : IIdentity
    {
        public string AuthenticationType => throw new NotImplementedException();

        public bool IsAuthenticated => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();
        public string UserData { get; set; }
    }
}
