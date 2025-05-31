using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditDivineEq
{

    public class QuerySetList
    {
        public readonly List<QuerySet> QueryLists = new List<QuerySet>();

        public QuerySetList()
        {
        }

        public void AddQuerySet(QuerySet querySet)
        {
            QueryLists.Add(querySet);
        }

        public void ClearMem()
        {
            QueryLists.Clear();
        }

    }

}
