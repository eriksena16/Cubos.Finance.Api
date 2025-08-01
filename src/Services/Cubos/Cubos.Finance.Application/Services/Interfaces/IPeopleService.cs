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
        //Task<PeopleResponse> GetByIdAsync(Guid id);
        //Task<IEnumerable<PeopleResponse>> GetAllAsync();
        //Task<PeopleResponse> UpdateAsync(Guid id, PeopleRequest request);
        //Task<bool> DeleteAsync(Guid id);
    }
}
