using Bomber.BL.Tiles;
using Infrastructure.Repositories;
using Newtonsoft.Json;

namespace Bomber.BL.MapGenerator
{
    public interface IMapLayoutDraft
    {
        Guid Id { get; }
        string Name { get; set; }
        string Description { get; set; }
        int ColumnCount { get; set; }
        int RowCount { get; set; }
        string RawData { get; }
        
        IEnumerable<IPlaceHolder>? MapObjects { get; }
        void SaveLayout(IEnumerable<IPlaceHolder> newMapObjects);
        void Delete();
    }
}
