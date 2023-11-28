using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.BL.MapGenerator.DomainModels;
using GameFramework.Core.Factories;
using GameFramework.Core.Position;
using GameFramework.Entities;
using GameFramework.Impl.Map.Source;
using GameFramework.Map.MapObject;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.BL.Impl.Map
{
    public class BomberMapSource : JsonMapSource2D, IBomberMapSource
    {
        private readonly IPositionFactory _factory;
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<DummyEntity> Enemies { get; set; }
        public IEnumerable<DummyBomb> Bombs { get; set; }
        public IPosition2D PlayerPosition { get; set; }


        public BomberMapSource(IServiceProvider provider, string filePath, int[,] data, ICollection<IUnit2D> units, int col, int row, string name, string description) : base(provider, filePath, data, units, col, row)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Enemies = units.Select(unit => new DummyEntity(unit.Position.X, unit.Position.Y));
            Bombs = new List<DummyBomb>();
            _factory = provider.GetRequiredService<IPositionFactory>();
            PlayerPosition = _factory.CreatePosition(1, 1);
        }

        public BomberMapSource(IServiceProvider provider, string filePath) : base(provider, filePath)
        {
            Name = Query.GetStringAttribute("name") ?? throw new InvalidOperationException("Map name is not specified");
            Description = Query.GetStringAttribute("description") ?? throw new InvalidOperationException("Map description is not specified");
            Enemies = Query.GetObject<List<DummyEntity>>("enemies") ?? new List<DummyEntity>();
            Bombs = Query.GetObject<List<DummyBomb>>("bombs") ?? new List<DummyBomb>();
            var x = Query.GetIntAttribute("player.x") ?? 1;
            var y = Query.GetIntAttribute("player.y") ?? 1;
            _factory = provider.GetRequiredService<IPositionFactory>();
            PlayerPosition = _factory.CreatePosition(x, y);
        }

        public override void SaveLayout(IEnumerable<IMapObject2D> updatedMapObjects, IEnumerable<IUnit2D> updatedUnits)
        {
            var units = updatedUnits.ToList();
            var player = units.FirstOrDefault(u => u is IBomber);
            if(player is not null)
            {
                Query.SetAttribute("player.x", player.Position.X);
                Query.SetAttribute("player.y", player.Position.Y);
            }
            
            Enemies = units.Where(u => u is IEnemy).Select(unit => new DummyEntity(unit.Position.X, unit.Position.Y));
            Bombs = units.Where(u => u is IBomb).Select(unit => new DummyBomb(unit.Position.X, unit.Position.Y, (unit as IBomb)!.Radius, (unit as IBomb)!.RemainingTime));
            Query.SetObject("enemies", Enemies);
            Query.SetObject("bombs", Bombs);
        }
    }
}
