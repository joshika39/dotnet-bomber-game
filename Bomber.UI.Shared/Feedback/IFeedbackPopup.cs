using GameFramework.Core;
using GameFramework.Manager.State;

namespace Bomber.UI.Shared.Feedback
{
    public interface IFeedbackPopup : IGameStateChangedListener
    {
        void DisplayError(string message, string title);
        void DisplaySuccess(string message, string title);
        void DisplayWarning(string message, string title);
        void DisplayInfo(string message, string title);
    }
}
