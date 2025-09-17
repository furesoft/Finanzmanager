using Avalonia.Controls;

namespace Finanzmanager.Contracts;

public interface INotificationService
{
    int NotificationTimeout { get; set; }

    void SetHostWindow(TopLevel window);

    void Show(string title, string message, Action? onClick = null);
}
