namespace Cubos.Finance.Shared
{
    public interface INotifier
    {
        bool HasNotification();

        List<Notification> GetNotifications();

        void Handle(Notification notification);
    }
}
