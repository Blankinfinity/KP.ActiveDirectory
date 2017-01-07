using System.Collections.Generic;
using System.DirectoryServices;

namespace KP.ActiveDirectory
{
    class DomainControllers
    {
        public List<string> GetDomainControllers()
        {
            List<string> DcList = new List<string>();

            using (DirectorySearcher Searcher = new DirectorySearcher(new DirectoryEntry()))
            {
                Searcher.Filter = "(primaryGroupID=516)";
                foreach (SearchResult DC in Searcher.FindAll())
                {
                    DcList.Add(DC.GetDirectoryEntry().Name.Remove(0, 3));
                }
            }
            return DcList;
        }

    }
}
