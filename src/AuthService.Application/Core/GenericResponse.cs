using AuthService.Application.Notifications;

namespace AuthService.Application.Core
{
    public class GenericResponse
    {
        public dynamic? Result { get; set; } = default;

        public IEnumerable<Notification>? Notifications { get; set; }
    }
}
