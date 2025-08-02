namespace Cubos.Finance.Shared
{
    public class QueryBaseResponse<T> : QueryBaseResponseFields<T>
    {
        public QueryBaseResponse(IQueryObject<T> filter)
        {
            if (filter.CurrentPage == 0)
                filter.CurrentPage = 1;

            if (filter.ItemsPerPage == 0)
                filter.ItemsPerPage = 10;


            CurrentPage = filter.CurrentPage;
            ItemsPerPage = filter.ItemsPerPage;
        }

    }

}
