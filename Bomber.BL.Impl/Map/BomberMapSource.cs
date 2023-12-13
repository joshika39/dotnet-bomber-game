using System.Text;
using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.BL.MapGenerator.DomainModels;
using GameFramework.Core.Factories;
using GameFramework.Core.Position;
using GameFramework.Entities;
using GameFramework.Impl.Map.Source;
using GameFramework.Map.MapObject;
using Infrastructure.IO;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.BL.Impl.Map
{
    public class BomberMapSource : JsonMapSource2D<TileType>, IBomberMapSource
    {
        private readonly IReader _testReader;
        private readonly IPositionFactory _fact;
        private readonly IMapObject2DConverter _converter;
        private readonly string _base64;
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<DummyEntity> Enemies { get; set; }
        public IEnumerable<DummyBomb> Bombs { get; set; }
        public IPosition2D PlayerPosition { get; set; }


        public BomberMapSource(IServiceProvider provider, string filePath, int[,] data, ICollection<IUnit2D> units, ICollection<DummyEntity> dummyEntities, int col, int row, string name, string description) : base(provider, filePath, data, units, col, row)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _testReader = provider.GetRequiredService<IReader>();
            _fact = provider.GetRequiredService<IPositionFactory>();
            _converter = provider.GetRequiredService<IMapObject2DConverter>();
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Enemies = dummyEntities;
            Bombs = new List<DummyBomb>();
            var factory = provider.GetRequiredService<IPositionFactory>();
            PlayerPosition = factory.CreatePosition(1, 1);
            _base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(GetRawData(data)));
            SaveInitState();
        }

        public BomberMapSource(IServiceProvider provider, string filePath) : base(provider, filePath)
        {
            Name = Query.GetStringAttribute("name") ?? throw new InvalidOperationException("Map name is not specified");
            Description = Query.GetStringAttribute("description") ?? throw new InvalidOperationException("Map description is not specified");
            Enemies = Query.GetObject<List<DummyEntity>>("enemies") ?? new List<DummyEntity>();
            Bombs = Query.GetObject<List<DummyBomb>>("bombs") ?? new List<DummyBomb>();
            var x = Query.GetIntAttribute("player.x") ?? 1;
            var y = Query.GetIntAttribute("player.y") ?? 1;
            var factory = provider.GetRequiredService<IPositionFactory>();
            PlayerPosition = factory.CreatePosition(x, y);
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

        private void SaveInitState()
        {
            Query.SetAttribute("player.x", PlayerPosition.X);
            Query.SetAttribute("player.y", PlayerPosition.Y);
            Query.SetAttribute("description", Description);
            Query.SetAttribute("name", Name);
            Query.SetAttribute("row", RowCount);
            Query.SetAttribute("col", ColumnCount);
            Query.SetAttribute("data", _base64);
            Query.SetObject("enemies", Enemies);
            Query.SetObject("bombs", Bombs);
        }
        
        protected virtual IEnumerable<IMapObject2D> ConvertDataToObjects()
        {
            var id = Guid.NewGuid();
            var tempPath = Path.Join(Path.GetTempPath(), $"{id}.txt");
            File.Create(tempPath).Close();
            File.WriteAllText(tempPath, Encoding.UTF8.GetString(Convert.FromBase64String(_base64)));
            using var stream = new StreamReader(tempPath);
            var mapLayout = _testReader.ReadAllLines<int>(stream, int.TryParse, ' ').ToList();
            var list = new List<IMapObject2D>();
            for (var i = 0; i < mapLayout.Count; i++)
            {
                var row = mapLayout[i].ToList();
                for (var j = 0; j < row.Count; j++)
                {
                    var value = row[j];
                    var position = _fact.CreatePosition(j, i);
                    if (!Enum.TryParse(value.ToString(), out TileType type))
                    {
                        continue;
                    }
                    list.Add(_converter.FromEnum(type, position));
                }
            }
            return list;
        }
        
        private string GetRawData(int[,] data)
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    var tile = data[i,j];
                    stringBuilder.Append($"{tile} ");
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append("\r\n");
            }
            return stringBuilder.ToString();
        }
    }
}
