using System.Text;
using System.Resources;
using System.Globalization;
using EditDivineEq;
using EditDivineEq.Resources;

using System.Security.Cryptography;

namespace WenWangWasm.Services
{

    public enum EditState : uint
    {
        Emty_Query = 0,
        Select_Query,
        Select_NewToEdit,
        Save_EqQuery_Ok,
        Revise_ExistedEqQuery,
        Revise_EditingEqQuery,
        Revise_EqQuery_Ok
    }

    public class QueryService : IQueryService
    {
        List<String> szAsks = new List<string>() { "Good", "School", "Trip", "Wealth", "FengShui",
                                                                              "Job", "Romance", "Marry", "Family", "Health",
                                                                              "House", "Law",  "Business", "Entreprenur" };

        int[] NumGroups = new int[14] { 3, 3, 1, 1, 1,
                                                             8, 3, 3, 3, 5,
                                                             5, 4, 9, 3};
        DivineKey[] Keys = new DivineKey[14] { DivineKey.Good,
                                                                         DivineKey.School,
                                                                         DivineKey.Trip,
                                                                         DivineKey.Wealth,
                                                                         DivineKey.FengShui,
                                                                         DivineKey.Job,
                                                                         DivineKey.Romance,
                                                                         DivineKey.Marry,
                                                                         DivineKey.Family,
                                                                         DivineKey.Health,
                                                                         DivineKey.House,
                                                                         DivineKey.Law,
                                                                         DivineKey.Business,
                                                                         DivineKey.Entreprenur };

        //all Resource in Models/EditQuery project
        static ResourceManager[] ResxMngrs = new ResourceManager[14]
                                               { GoodText.ResourceManager,
                                                 SchoolText.ResourceManager,
                                                 TripText.ResourceManager,
                                                 WealthText.ResourceManager,
                                                 FengShuiText.ResourceManager,
                                                 JobText.ResourceManager,
                                                 RomanceText.ResourceManager,
                                                 MarryText.ResourceManager,
                                                 FamilyText.ResourceManager,
                                                 HealthText.ResourceManager,
                                                 HouseText.ResourceManager,
                                                 LawText.ResourceManager,
                                                 BusinessText.ResourceManager,
                                                 EntreprenurText.ResourceManager };
                                                //PoliticsText.ResourceManager,
                                                //EconomicsText.ResourceManager,

        public CultureInfo Language { get; set; }

        public List<AskList> AskLists { get; set; } = null;
        public List<Dictionary<string, string>> MenuLsit { get; set; } = null;
        public QuerySet DisplayQuerySet { get; set; } = null;

        public Ask SelectAsk { get; set; } = null;
        public Query SelectQuery { get; set; } = null;

        public List<Query> EventQueries { get; set; } = null;

        QuerySetList[] GroupQuery { get; set; } = null;

        public QueryService()
        {

            AskLists = new List<AskList>();
            GroupQuery = new QuerySetList[14] { null, null, null, null, null, null,
                                                                          null, null, null, null, null, null, null, null };

        }

        public List<AskList>  GetDivineQuerySubject()
        {
            CultureInfo UseCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = UseCulture;
            Thread.CurrentThread.CurrentUICulture = UseCulture;

            SetAskLists();
            return AskLists;
        }

        public void CheckQuerySetNotExisting_ToReadQuerySet()
        {
            CultureInfo UseCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = UseCulture;
            Thread.CurrentThread.CurrentUICulture = UseCulture;

            if (SelectAsk != null)
            {
                if (DisplayQuerySet == null ||
                     (DisplayQuerySet != null && DisplayQuerySet.KeyIdx != SelectAsk.KeyIdx))
                    ReadSelectQueryStrings();
                GetDisplayQueryStrings();
            }
        }

        public void GetDisplayQueryStrings()
        {
            DisplayQuerySet = GroupQuery[SelectAsk.KeyIdx].QueryLists[SelectAsk.KeySubIdx];
        }

        private void ReadSelectQueryStrings()
        {
            if (SelectAsk != null)
            {
                GroupQuery[SelectAsk.KeyIdx] = new QuerySetList();
                for (int GroupIdx = 0; GroupIdx < NumGroups[SelectAsk.KeyIdx]; GroupIdx++)
                    GroupQuery[SelectAsk.KeyIdx].AddQuerySet(ReadQueryResourceStrings(GroupIdx));
            }
        }

        private QuerySet ReadQueryResourceStrings(int GroupIdx)
        {
            string szQuery, szName, szBaseName, szIndex;
            bool bContinue;
            int Index;
            Query query;

            string GroupName = szAsks[SelectAsk.KeyIdx];
            QuerySet querySet = new QuerySet(SelectAsk);
            szBaseName = GroupName + GroupIdx.ToString();
            Index = -1;
            do{
                Index++;
                szIndex = Index.ToString();
                szName = szBaseName + ((Index < 10) ? ("0" + szIndex) : szIndex);
                //szQuery = ResxMngrs[ SelectAsk.KeyIdx ].GetString(szName, Language);
                szQuery = ResxMngrs[SelectAsk.KeyIdx].GetString(szName);//, Language);
                bContinue = szQuery.Length > 0 ? true : false;
                if (bContinue)
                {
                    query = new Query(SelectAsk.DivineKeyID, GroupIdx, Index, szQuery);
                    querySet.AddQuery(query);
                }
            } while (bContinue);

            return querySet;
        }

        void SetAskLists()
        {
            string szString, szString_x, Title;
            AskList Asks;
            Ask Query;

            for (int m = 0; m < Keys.Length; m++)//Keys.Length = 14
            {
                szString = szAsks[m];
                szString_x = szString + "0";
                //Title = SubjectRes.ResourceManager.GetString(szString_x, SubjectRes.Culture);
                Title = SubjectText.ResourceManager.GetString(szString_x);//, SubjectRes.Culture);
                AskLists.Add(new AskList(Title));
                Asks = AskLists[m];
                for (int n = 0; n < NumGroups[m]; n++)
                {
                    szString_x = szString + (n + 1).ToString();
                    //Bits(unit) DivineKey.Good, DivineKey.School,....  ,DivineKey.Business, DivineKey.Entreprenur;
                    //Query = new Ask( m, Keys[m], n, SubjectRes.ResourceManager.GetString(szString_x, SubjectRes.Culture));
                    Query = new Ask(m, Keys[m], n, SubjectText.ResourceManager.GetString(szString_x));//, SubjectRes.Culture));
                    Asks.AddAsk(Query);
                }
            }

        }

        public void GetTransferAskListsToMenus()
        {
            List<Dictionary<string, string>> MenuLsit = new List<Dictionary<string, string>>();
            Dictionary<string, string> SubMenus;

            foreach (AskList SubAsks in AskLists)
            {
                SubMenus = new Dictionary<string, string>();
                foreach (Ask ask in SubAsks.Asks)
                    SubMenus.Add( ask.KeySubIdx.ToString(), ask.szAsk );
                MenuLsit.Add( SubMenus );
            }

        }

    }
}
