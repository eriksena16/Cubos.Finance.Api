using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public static class QueryBaseResponseExtensions
    {
        public static QueryBaseResponse<TDestination> MapItems<TSource, TDestination>(
            this QueryBaseResponse<TSource> source,
            Func<TSource, TDestination> mapFunc)
        {
            return new QueryBaseResponse<TDestination>(new DummyQueryObject<TDestination>
            {
                CurrentPage = source.CurrentPage,
                ItemsPerPage = source.ItemsPerPage
            })
            {
                TotalItems = source.TotalItems,
                Items = source.Items.Select(mapFunc)
            };
        }

        private class DummyQueryObject<T> : IQueryObject<T>
        {
            public int CurrentPage { get; set; }
            public int ItemsPerPage { get; set; }
            public int TotalItems { get; set; }
        }
    }

}
