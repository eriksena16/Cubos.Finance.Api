namespace Cubos.Finance.Shared
{
    public class QueryBaseResponseFields<TEntity>
    {
        public IEnumerable<TEntity> Items { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get
            {
                if (TotalItems == 0)
                    return 0;

                return (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
            }
        }

    }
}
