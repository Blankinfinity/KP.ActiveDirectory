using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.ActiveDirectory
{
    class ADUsers
    {
        /// <summary>
        /// Method to validate if a user exists in the AD.
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool UserExists(string UserName)
        {
            DirectoryEntry de = ADConnect.GetDirectoryEntry();
            DirectorySearcher deSearch = new DirectorySearcher();
            deSearch.SearchRoot = de;
            deSearch.Filter = "(&(objectClass=user) (cn=" + UserName + "))";
            SearchResultCollection results = deSearch.FindAll();
            if (results.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Helper method that sets properties for AD users.
        /// </summary>
        /// <param name="de"></param>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        public static void SetProperty(DirectoryEntry de, string PropertyName, string PropertyValue)
        {
            if (PropertyValue != null)
            {
                if (de.Properties.Contains(PropertyName))
                {
                    de.Properties[PropertyName][0] = PropertyValue;
                }
                else
                {
                    de.Properties[PropertyName].Add(PropertyValue);
                }
            }
        }

        /// <summary>
        /// Method to enable a user account in the AD.
        /// </summary>
        /// <param name="de"></param>
        private static void EnableAccount(DirectoryEntry de)
        {
            //UF_DONT_EXPIRE_PASSWD 0x10000
            int exp = (int)de.Properties["userAccountControl"].Value;
            de.Properties["userAccountControl"].Value = exp | 0x0001;
            de.CommitChanges();
            //UF_ACCOUNTDISABLE 0x0002
            int val = (int)de.Properties["userAccountControl"].Value;
            de.Properties["userAccountControl"].Value = val & ~0x0002;
            de.CommitChanges();
        }

        /// <summary>
        /// Method that disables a user account in the AD and hides user's email from Exchange address lists.
        /// </summary>
        /// <param name="EmployeeID"></param>
        public void DisableAccount(string EmployeeID)
        {
            DirectoryEntry de = ADConnect.GetDirectoryEntry();
            DirectorySearcher ds = new DirectorySearcher(de);
            ds.Filter = "(&(objectCategory=Person)(objectClass=user)(employeeID=" + EmployeeID + "))";
            ds.SearchScope = SearchScope.Subtree;
            SearchResult results = ds.FindOne();
            if (results != null)
            {
                DirectoryEntry dey = ADConnect.GetDirectoryEntryAtPath(results.Path);
                int val = (int)dey.Properties["userAccountControl"].Value;
                dey.Properties["userAccountControl"].Value = val | 0x0002;
                dey.Properties["msExchHideFromAddressLists"].Value = "TRUE";
                dey.CommitChanges();
                dey.Close();
            }
            de.Close();
        }
    }
}
