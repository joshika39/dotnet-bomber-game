using Bomber.BL.Map;
using Bomber.UI.Shared.Views;

namespace Bomber.BL.Impl.Models
{
    public interface IMainWindowModel
    {
        IBomberMap OpenMap(string mapFileName);
        IMainWindow? View { get; }
    }
}
