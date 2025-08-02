using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubos.Finance.Domain
{
    public interface IPeopleRepository
    {
        Task<People> CreateAsync(People request);
        Task<People> GetByDocumentAsync(string document);
        Task<bool> HasPeopleAsync(string document);
    }
}
