using Bomber.BL.Impl.Models;

namespace Bomber.UI.WPF.ViewModels
{
    public interface IMainWindowViewModel : IMainWindowModel
    {
        object DataContext { get; }
    }
}
