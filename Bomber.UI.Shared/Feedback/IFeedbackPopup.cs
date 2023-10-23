using GameFramework.Core;

namespace Bomber.UI.Shared.Feedback
{
    public interface IFeedbackPopup : IGameManagerSubscriber
    {
        void DisplayError(string message, string title);
        void DisplaySuccess(string message, string title);
        void DisplayWarning(string message, string title);
        void DisplayInfo(string message, string title);
    }
}
