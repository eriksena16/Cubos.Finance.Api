using FluentValidation;
using FluentValidation.Results;

namespace Cubos.Finance.Shared
{
    public abstract class ServiceBase
    {
        private readonly INotifier _notifier;
        public ServiceBase(INotifier notifier)
        {
            _notifier = notifier;
        }
        protected void Notify(ValidationResult validationResult)
            => validationResult.Errors.ForEach(erro => Notify(erro.ErrorMessage));

        protected void Notify(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }
        protected bool IsInvalidOperation()
        {
            return _notifier?.HasNotification() ?? false;
        }

        protected bool ExecutarValidacao<TValidation, TEntity>(TValidation validation, TEntity entity)
            where TValidation : AbstractValidator<TEntity>
            where TEntity : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }

    }
}
