using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cubos.Finance.Shared
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private readonly INotifier _notifier;
        public ApiControllerBase(INotifier notifier)
        {
            _notifier = notifier;
        }
        protected bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
                return Ok(new
                {
                    Success = true,
                    Data = result
                });

            return BadRequest(new
            {
                Success = false,
                resons = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!ModelState.IsValid) NotifyErrorModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotifyErrorModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }


    }
}
