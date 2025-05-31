using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditDivineEq
{

    public class Query
    {
        public DivineKey DivineKeyID// enum DivineKey : uint Query category
        { get; set; }
        public int KeySubIdx//Query subject index
        { get; set; }
        public int QueryIdx//query list index
        { get; set; }
        public string QueryIdxSz//query list index
        { get; set; }

        public string QuerySz//Verbal query 
        { get; set; }
        public string EdString//for input data lable [xxxx,xxxx(Y),xxxx(T)] from "[" to "]"
        { get; set; }
        public string OpString//<S/Y/W@+>
        { get; set; }//for divine model analysis : S/Y/CW 世爻;應爻;子孫;妻財;
        public string ModelString//for DivineModel{A},{AR},{OR}  
        { get; set; }

        public string Sub1Querysz//additional query
        { get; set; }
        public string Sub2Querysz//additional query
        { get; set; }

        public bool B_2Compare
        { get; set; }

        public string Pricesz
        { get; set; }

        //public string szDivineModelString { get; set; }//for input data lable

        public Query(DivineKey DivineKey_ID, int Key_SubIdx, int Query_Idx, string szResx)
        {
            DivineKeyID = DivineKey_ID;
            KeySubIdx = Key_SubIdx;
            QueryIdx = Query_Idx;
            QueryIdxSz = (QueryIdx + 1).ToString() + ". ";

            CutStringData(szResx);
        }

        public Query(Query x)
        {
            DivineKeyID = x.DivineKeyID;
            KeySubIdx = x.KeySubIdx;
            QueryIdx = x.QueryIdx;
            QuerySz = x.QuerySz;

            Sub1Querysz = x.Sub1Querysz;
            Sub2Querysz = x.Sub2Querysz;

            OpString = x.OpString;
            B_2Compare = x.B_2Compare;
            Pricesz = x.Pricesz;
        }

        void CutStringData(string szResx)
        {
            //Query string  pattern
            //abcdefg hijklm ?[xyz,stuh & sxyb,wxyz(Y)]{ARym}<S/O/W@+/C>100
            int BgnAtIdx, EndAtIdx, subLength, SeparateAtIdx;

            if ((SeparateAtIdx = szResx.IndexOf('^')) != -1)//'^' separator
            {
                SeparateAtIdx -= 1; B_2Compare = true;
            }
            else
            {
                SeparateAtIdx = 0; B_2Compare = false;
            }

            //string pattern  ....[Lable 1]...[Lable 2]...[Lable 3]........ ?[Lable 1,Lable 2,Lable 3]{FRm}<S@CW>200.0

            //(1) to cut szQuery....'?' is the separator
            //'?' cut the string from 0....  to '?' is the query string
            EndAtIdx = szResx.IndexOf('?', SeparateAtIdx);
            subLength = EndAtIdx + 1;
            QuerySz = szResx.Substring(0, subLength);//Begin Index, sub-string length

            if (B_2Compare)
            {
                BgnAtIdx = szResx.IndexOf('1', EndAtIdx + 1);
                EndAtIdx = szResx.IndexOf('?', BgnAtIdx);
                subLength = EndAtIdx - BgnAtIdx + 1;
                Sub1Querysz = szResx.Substring(BgnAtIdx, subLength);//Begin Index, sub-string length

                BgnAtIdx = szResx.IndexOf('2', EndAtIdx + 1);
                EndAtIdx = szResx.IndexOf('?', BgnAtIdx);
                subLength = EndAtIdx - BgnAtIdx + 1;
                Sub2Querysz = szResx.Substring(BgnAtIdx, subLength);//Begin Index, sub-string length

                subLength = EndAtIdx + 1;
            }

            //(2) to cut szEdString for user-------- Lables
            BgnAtIdx = subLength + 1;//index after '?' is '['
            EndAtIdx = szResx.IndexOf(']', subLength);//search from subLength-index(after the '?' separator)
            subLength = EndAtIdx - BgnAtIdx;
            EdString = szResx.Substring(BgnAtIdx, subLength);//begin index after '[', SubLength 

            BgnAtIdx = EndAtIdx + 1;//index after ']' is '{'
            EndAtIdx = szResx.IndexOf('}', BgnAtIdx);//search from subLength-index(after the '?' separator)
            subLength = EndAtIdx - BgnAtIdx - 1;
            ModelString = szResx.Substring(BgnAtIdx + 1, subLength);//begin index after '{', SubLength 

            BgnAtIdx = EndAtIdx + 1;//index after '{' is '<'
            EndAtIdx = szResx.IndexOf('>', BgnAtIdx);//search from subLength-index(after the '?' separator)
            subLength = EndAtIdx - BgnAtIdx - 1;
            OpString = szResx.Substring(BgnAtIdx + 1, subLength);//begin index after '{', SubLength 

            BgnAtIdx = EndAtIdx + 1;//index after '>' is 1St digit
            subLength = szResx.Length - BgnAtIdx;
            Pricesz = szResx.Substring(BgnAtIdx, subLength);//begin index after '{', SubLength 

        }

    }

}
