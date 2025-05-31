using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditDivineEq
{

    public class QuerySet
    {
        public int KeyIdx
        { get; set; }
        public DivineKey DivineKeyID// enum DivineKey : uint Query category
        { get; set; }
        public int KeySubIdx//Query subject index
        { get; set; }
        public readonly List<Query> Queries = new List<Query>();

        public QuerySet(Ask ask)
        {
            KeyIdx = ask.KeyIdx;
            DivineKeyID = ask.DivineKeyID;
            KeySubIdx = ask.KeySubIdx;
        }

        public void AddQuery(Query query)
        {
            Queries.Add(query);
        }

        public void ClearMem()
        {
            Queries.Clear();
        }

    }

}
