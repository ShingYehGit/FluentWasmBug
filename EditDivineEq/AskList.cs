using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditDivineEq
{

    public class AskList
    {

        public AskList(string title)
        {
            Title = title;
            Asks = new List<Ask>();
        }

        public string Title { get; set; }
        public List<Ask> Asks { get; set; }

        public void AddAsk(Ask query)
        {
            Asks.Add(query);
        }

        public Ask GetAsk(int Index)
        {
            int nIdx = -1;
            foreach (Ask q in Asks)
            {
                if (++nIdx == Index)
                    return q;
            }
            return null;
        }

        public void ClearMem()
        {
            Asks.Clear();
        }

    }

}
