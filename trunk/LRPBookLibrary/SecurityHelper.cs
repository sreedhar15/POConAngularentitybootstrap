using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Security;
using System.Security.Principal;
using System.Runtime.InteropServices;


namespace LRPBookLibrary
{
    public class UserCredential
    {
        public string LoginName { get; set; }
        public string Domain { get; set; }
        public string Password { get; set; }
    }

    
    /// <summary>
    /// This class defines windows security related utility functions
    /// Author Rajkamal Gopinath
    /// Date 10/28/2013
    /// </summary>
    public static class SecurityHelper
    {
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        private static WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        /// This method impersonates a window domain account for the current process.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="domain"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ImpersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        /// <summary>
        /// This method cancels the windows impersonation.
        /// </summary>
        public static void undoImpersonation()
        {
            if (impersonationContext != null)
                impersonationContext.Undo();
        }

       
      
        /// <summary>
        /// This method authenticates user credentials against LDAP / Action Directory
        /// </summary>
        /// <param name="ldapPath"></param>
        /// <param name="domainAndUsername"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool LDAPCheck(string ldapPath, string domainAndUsername, string username, string password)
        {
            DirectoryEntry entry;
            if (string.IsNullOrEmpty(password))
                entry = new DirectoryEntry(ldapPath);
            else
                entry = new DirectoryEntry(ldapPath, domainAndUsername, password);
            try
            {
                // Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (result == null)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identityName"></param>
        /// <returns></returns>
        public static UserCredential GetUserCredential(string identityName) {
            
            UserCredential userCredential=new UserCredential();
            
            if (identityName.IndexOf('|')>=0)
            {   
                string[] splitData = identityName.Split(new char[] { '|' });
                userCredential.LoginName = splitData[0].ToLower();
                userCredential.Password = splitData[1];
                
            }
            else
            {
                string[] splitData = identityName.Split(new char[] { '\\' });
                userCredential.Domain = splitData[0].ToLower();
                userCredential.LoginName = splitData[1].ToLower();
            }
            
            return userCredential;
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identityName"></param>
        /// <returns></returns>
        public static string GetUserName(string identityName)
        {   string[] splitData = identityName.Split(new char[] { '\\' });
            return  splitData[1].ToLower();
        }

        /// <summary>
        /// This method returns the status of whether a user is valid and found in the LDAP/Active directory.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsValidLDAPUser(string username, string password)
        {
            string LDAPPATH = string.Format("LDAP://dc={0},dc=com", username.Split(char.Parse("\\"))[0].ToLower());
            bool status = LDAPCheck(LDAPPATH, username, username.Split(char.Parse("\\"))[1], password);

            if (!status)
            {
                return false;
            }

            return true;
        }

       

    }
}
