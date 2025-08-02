using System.Linq.Expressions;

namespace Cubos.Finance.Shared
{
    public interface IQueryObject<TEntity>
    {
        int CurrentPage { get; set; }
        int ItemsPerPage { get; set; }
        Dictionary<string, Expression<Func<TEntity, object>>> Map();
    }
}
