using System.Windows;
using Bomber.UI.Shared.Feedback;
using GameFramework.GameFeedback;
using GameFramework.Manager;

namespace Bomber.UI.WPF
{
    internal class WpfFeedbackPopup : IFeedbackPopup
    {

        public void OnGamePaused()
        { }
        
        public void OnGameResumed()
        { }
        
        public void OnGameStarted(IGameplayFeedback feedback)
        { }
        
        public void OnGameFinished(IGameplayFeedback feedback, GameResolution resolution)
        {
            switch (resolution)
            {
                case GameResolution.Win:
                case GameResolution.Loss:
                    DisplayInfo(feedback.Message, "Game finished!");
                    break;
            }
        }
        
        public void OnGameReset()
        {
            
        }
        
        public void DisplayError(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        public void DisplaySuccess(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        public void DisplayWarning(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        
        public void DisplayInfo(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
