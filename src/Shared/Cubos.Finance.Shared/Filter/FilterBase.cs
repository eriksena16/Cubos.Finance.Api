using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubos.Finance.Shared
{
    public abstract class FilterBase<TEntity>
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }

    }
}
