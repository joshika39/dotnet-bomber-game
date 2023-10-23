using GameFramework.Core;
using GameFramework.Map.MapObject;
using Infrastructure.Repositories;

namespace Bomber.BL.Map
{
    public interface IMapLayout
    {
        Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
        IEnumerable<IMapObject2D> MapObjects { get; }
        IPosition2D PlayerPosition { get; set; }
    }
}
