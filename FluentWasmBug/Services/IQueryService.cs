using EditDivineEq;
using System.Globalization;

namespace WenWangWasm.Services
{

    public interface IQueryService
    {

        CultureInfo Language { get; set; }

        List<AskList> AskLists { get; set; }
        List<Dictionary<string, string>> MenuLsit { get; set; }
        QuerySet DisplayQuerySet { get; set; }
        Ask SelectAsk { get; set; }
        Query SelectQuery { get; set; }

        List<Query> EventQueries { get; set; }

        List<AskList> GetDivineQuerySubject();
        void GetTransferAskListsToMenus();
        void CheckQuerySetNotExisting_ToReadQuerySet();
        //void SetAskLists();

    }

}
