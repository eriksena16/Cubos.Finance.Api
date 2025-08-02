using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubos.Finance.Application
{
    public interface IPeopleService
    {
        Task<PeopleResponse> CreateAsync(PeopleRequest request);

    }
}
