using Bomber.BL.MapGenerator.DomainModels;
using Bomber.BL.Tiles;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Entities;
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
        int[,] Data { get; }
        ICollection<DummyEntity> Entities { get; }
        IPosition2D PlayerStartPosition { get; set; }
        IEnumerable<IPlaceHolder>? MapObjects { get; }
        void SaveLayout(IEnumerable<IPlaceHolder> newMapObjects);
        void Delete();
    }
}
