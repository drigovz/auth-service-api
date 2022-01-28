using FluentValidation.Results;

namespace AuthService.Application.Notifications
{
    public class NotificationContext
    {
        private readonly List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();

        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public List<Notification> AddNotification(string key, string message)
        {
            _notifications.Add(new Notification(key, message));
            return _notifications;
        }

        public List<Notification> AddNotifications(IReadOnlyCollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
            return _notifications;
        }

        public List<Notification> AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                AddNotification(error.ErrorCode, error.ErrorMessage);

            return _notifications;
        }
    }
}
