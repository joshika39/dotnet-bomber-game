using Bomber.BL.Entities;
using GameFramework.Core;
using GameFramework.Map.MapObject;
using Bomber.BL.MapGenerator.DomainModels;

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
        ICollection<DummyEntity> Entities { get; set; }
        IPosition2D PlayerPosition { get; set; }
        int PlayerScore { get; set; }

        void SaveLayout(IBomber bomber, IList<DummyEntity> dummyEntities);
    }
}
