using Bomber.BL.Map;
using Bomber.BL.MapGenerator;
using UiFramework.Forms;

namespace Bomber.UI.Forms.MapGenerator
{
    public interface IMapGeneratorWindowPresenter : IWindowPresenter
    {
        IMapLayoutDraft SelectedDraft { get; set; }
        IEnumerable<IMapLayoutDraft> Drafts { get; }
        void UpdateDraft(IMapLayoutDraft draft);
        IMapLayoutDraft CreateDraft();
        void GenerateMapFromDraft(IMapLayoutDraft draft, string filename);
    }
}
