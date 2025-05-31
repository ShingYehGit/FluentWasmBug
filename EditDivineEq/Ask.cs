using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditDivineEq
{

    public enum DivineKey : uint
    {
        None = 0,
        Good = (uint)(1),
        School = ((uint)1 << 2),
        Trip = ((uint)1 << 3),
        Wealth = ((uint)1 << 4),
        FengShui = ((uint)1 << 5),
        Job = ((uint)1 << 6),
        Romance = ((uint)1 << 7),
        Marry = ((uint)1 << 8),
        Family = ((uint)1 << 9),
        Health = ((uint)1 << 10),
        House = ((uint)1 << 11),
        Law = ((uint)1 << 12),
        Business = ((uint)1 << 13),
        Entreprenur = ((uint)1 << 14),
        Politics = ((uint)1 << 15),
        Economics = (uint)(1 << 16),
        PublicEvents = ((uint)1 << 17)
    }

    public class Ask
    {
        public int KeyIdx
        { get; set; }//same sequential index w.r.t DivineKey
        public DivineKey DivineKeyID// enum DivineKey : uint Bits merged Key
        { get; set; }
        public int KeySubIdx//Query subject index
        { get; set; }

        public string szAsk//Verbal query 
        { get; set; }

        public Ask()
        {
            KeyIdx = -1;
            DivineKeyID = DivineKey.None;
            KeySubIdx = -1;
            szAsk = string.Empty;
        }

        public Ask(int Key_Idx, DivineKey divineKeyID, int keySubIdx, string Ask)
        {
            KeyIdx = Key_Idx;
            DivineKeyID = divineKeyID;
            KeySubIdx = keySubIdx;
            szAsk = Ask;
        }

    }

}
