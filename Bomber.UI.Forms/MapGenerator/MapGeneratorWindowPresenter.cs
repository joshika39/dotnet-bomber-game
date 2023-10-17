using Bomber.BL.Map;
using Bomber.BL.MapGenerator;

namespace Bomber.UI.Forms.MapGenerator
{
    public class MapGeneratorWindowPresenter : IMapGeneratorWindowPresenter
    {
        private readonly IMapGeneratorSettings _mapGeneratorSettings;

        public IMapLayoutDraft SelectedDraft
        {
            get => _mapGeneratorSettings.SelectedDraft;
            set => _mapGeneratorSettings.SelectedDraft = value;
        }
        
        public IEnumerable<IMapLayoutDraft> Drafts => _mapGeneratorSettings.Drafts;

        public MapGeneratorWindowPresenter(IMapGeneratorSettings mapGeneratorSettings)
        {
            _mapGeneratorSettings = mapGeneratorSettings ?? throw new ArgumentNullException(nameof(mapGeneratorSettings));
            SelectedDraft = _mapGeneratorSettings.SelectedDraft;
        }
        
        public void UpdateDraft(IMapLayoutDraft draft)
        {
            _mapGeneratorSettings.UpdateDraft(draft);
        }

        public IMapLayoutDraft CreateDraft()
        {
            return _mapGeneratorSettings.CreateDraft();
        }
        public void GenerateMapFromDraft(IMapLayoutDraft draft, string name)
        {
            _mapGeneratorSettings.GenerateMapFromDraft(draft, name);
        }
    }
}
