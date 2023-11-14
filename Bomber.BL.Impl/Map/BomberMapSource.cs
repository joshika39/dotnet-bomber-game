using Bomber.BL.Map;
using Bomber.BL.MapGenerator.DomainModels;
using GameFramework.Entities;
using GameFramework.Impl.Map.Source;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Map
{
    public class BomberMapSource : JsonMapSource2D, IBomberMapSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<DummyEntity> Enemies { get; set; }
        public BomberMapSource(IServiceProvider provider, string filePath, int[,] data, ICollection<IUnit2D> units, int col, int row, string name, string description) : base(provider, filePath, data, units, col, row)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Enemies = units.Select(unit => new DummyEntity(unit.Position.X, unit.Position.Y));
        }

        public BomberMapSource(IServiceProvider provider, string filePath) : base(provider, filePath)
        {
            Name = Query.GetStringAttribute("name") ?? throw new InvalidOperationException("Map name is not specified");
            Description = Query.GetStringAttribute("description") ?? throw new InvalidOperationException("Map description is not specified");
            Enemies = Query.GetObject<List<DummyEntity>>("enemies") ?? new List<DummyEntity>();
        }

        public override void SaveLayout(IEnumerable<IMapObject2D> updatedMapObjects, IEnumerable<IUnit2D> updatedUnits)
        {
            var units = updatedUnits.ToList();
            Enemies = units.Select(unit => new DummyEntity(unit.Position.X, unit.Position.Y));
            Query.SetObject("enemies", Enemies);
        }
    }
}
