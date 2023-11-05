using Bomber.BL.Map;

namespace Bomber.BL.MapGenerator
{
    public interface IMapGeneratorSettings
    {
        IMapLayoutDraft SelectedDraft { get; set; }
        IEnumerable<IMapLayoutDraft> Drafts { get; }
        void UpdateDraft(IMapLayoutDraft draft);
        IMapLayoutDraft CreateDraft();
        void GenerateMapFromDraft(IMapLayoutDraft draft, string filename);
    }
}
