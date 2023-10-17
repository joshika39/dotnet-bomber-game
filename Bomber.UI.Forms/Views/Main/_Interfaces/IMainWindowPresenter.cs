using Bomber.BL.Impl.Models;
using UiFramework.Forms;
using IMainWindow = Bomber.UI.Forms.Main.IMainWindow;

namespace Bomber.UI.Forms.Views.Main._Interfaces
{
    public interface IMainWindowPresenter : IWindowPresenter, IMainWindowModel
    {
        void OpenMapGenerator();
    }
}
