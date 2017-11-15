﻿using System;
using System.Collections.Generic;
using System.Text;
#if NET461
    using UserAuthentication.Store.Database;
#endif

namespace UserAuthentication
{
    public class AuthenticationServiceFactory
    {
        public static AuthenticationService GetAuthenticationServiceFile(string path = @"C:\StoreFolder")
        {
            var fileStore = new AuthenticationDetailsStoreFile(path);

            return new AuthenticationService(fileStore);
        }

#if NET461
        public static AuthenticationService GetAuthenticationServiceDB()
        {
            var dbStore = new AuthenticationDetailsStoreDatabase();

            return new AuthenticationService(dbStore);
        }
#endif
    }
}
