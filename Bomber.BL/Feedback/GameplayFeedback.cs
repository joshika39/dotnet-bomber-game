using GameFramework.GameFeedback;

namespace Bomber.BL.Feedback
{
    public class GameplayFeedback : IGameplayFeedback
    {
        public GameplayFeedback(FeedbackLevel level, string message)
        {
            Level = level;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public FeedbackLevel Level { get; }
        public string Message { get; }
    }
}
