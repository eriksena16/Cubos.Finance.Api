

using Cubos.Finance.Application;
using Cubos.Finance.Shared;

namespace Cubos.Finance.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PeopleController : ApiControllerBase
    {
        private readonly IPeopleService _peopleService;
        public PeopleController(INotifier notifier, IPeopleService peopleService) : base(notifier)
        {
            _peopleService = peopleService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PeopleRequest request)
        {
            var response = await _peopleService.CreateAsync(request);

            return response != null
                ? Ok(response)
                : BadRequest();
        }

    }
}
