using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.ActiveDirectory
{
    class ADConnect
    {
        /// <summary>
        /// Method used to create an entry to the AD.
        /// Replace the path, username, and password.
        /// </summary>
        /// <returns>DirectoryEntry</returns>
        public static DirectoryEntry GetDirectoryEntry()
        {
            DirectoryEntry de = new DirectoryEntry();
            de.Path = "LDAP://192.168.1.1/CN=Users;DC=Yourdomain";
            de.Username = @"yourdomain\sampleuser";
            de.Password = "samplepassword";
            return de;
        }

        /// <summary>
        /// Method used to create an entry to the AD using a secure connection.
        /// Replace the path.
        /// </summary>
        /// <returns>DirectoryEntry</returns>
        public static DirectoryEntry GetSecureDirectoryEntry()
        {
            DirectoryEntry de = new DirectoryEntry();
            de.Path = "LDAP://192.168.1.1/CN=Users;DC=Yourdomain";
            de.AuthenticationType = AuthenticationTypes.Secure;
            return de;
        }

        public static DirectoryEntry GetDirectoryEntryAtPath(string path)
        {
            DirectoryEntry de = new DirectoryEntry();
            de.Path = path;
            de.Username = @"yourdomain\sampleuser";
            de.Password = "samplepassword";
            return de;
        }
    }
}
