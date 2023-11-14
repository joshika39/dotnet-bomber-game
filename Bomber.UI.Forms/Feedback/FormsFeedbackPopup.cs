using Bomber.UI.Shared.Feedback;
using GameFramework.GameFeedback;
using GameFramework.Manager;

namespace Bomber.UI.Forms.Feedback
{
    internal class FormsFeedbackPopup : IFeedbackPopup
    {
        public void DisplayError(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        public void DisplaySuccess(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        public void DisplayWarning(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        
        public void DisplayInfo(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
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
        
        public void OnGamePaused()
        {
            throw new NotImplementedException();
        }
        
        public void OnGameResumed()
        {
            throw new NotImplementedException();
        }
        
        public void OnGameStarted(IGameplayFeedback feedback)
        {
            
        }
        
        public void OnGameReset()
        {
            throw new NotImplementedException();
        }
    }
}
