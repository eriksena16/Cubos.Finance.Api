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
        protected void Notificar(ValidationResult validationResult)
            => validationResult.Errors.ForEach(erro => Notificar(erro.ErrorMessage));

        protected void Notificar(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }

        protected bool ExecutarValidacao<TValidation, TEntity>(TValidation validation, TEntity entity)
            where TValidation : AbstractValidator<TEntity>
            where TEntity : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }

    }
}
