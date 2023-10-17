using System.Text;
using Bomber.BL.Map;
using Bomber.BL.MapGenerator;
using Bomber.BL.Tiles.Factories;
using GameFramework.Configuration;
using GameFramework.Core.Factories;
using GameFramework.Map.MapObject;
using Infrastructure.Configuration;
using Infrastructure.Configuration.Factories;
using Infrastructure.IO;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.BL.Impl.Map
{
    public class MapLayout : IMapLayout
    {
        private readonly IConfigurationQuery _query;
        private readonly IConfigurationService2D _configurationService;
        private readonly ITileFactory _tileFactory;
        private readonly IReader _reader;
        private readonly string _mapDataBase64;
        private readonly IPositionFactory _positionFactory;
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
        public Guid Id { get; }
        public IEnumerable<IMapObject2D> MapObjects { get; }

        public MapLayout(
            string filePath,
            IServiceProvider provider)
        {
            Id = Guid.NewGuid();
            var queryFactory = provider.GetRequiredService<IConfigurationQueryFactory>();
            var filePath1 = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _configurationService = provider.GetRequiredService<IConfigurationService2D>();
            _query = queryFactory.CreateConfigurationQuery(filePath1);
            _tileFactory = provider.GetRequiredService<ITileFactory>();
            _positionFactory = provider.GetRequiredService<IPositionFactory>();
            _reader = provider.GetRequiredService<IReader>();

            Name = _query.GetStringAttribute("name") ?? throw new InvalidOperationException("Draft config is missing a 'name'");
            Description = _query.GetStringAttribute("description") ??  throw new InvalidOperationException("Draft config is missing a 'description'");
            ColumnCount = _query.GetIntAttribute("row") ??  throw new InvalidOperationException("Draft config is missing a 'row;");
            RowCount = _query.GetIntAttribute("col") ??  throw new InvalidOperationException("Draft config is missing a 'col'");
            _mapDataBase64 = _query.GetStringAttribute("data") ??  throw new InvalidOperationException("Draft config is missing the 'data'");
            MapObjects = ConvertDataToObjects();
        }
        
        public MapLayout(
            string filePath,
            IMapLayoutDraft source,
            IServiceProvider provider)
        {
            Id = Guid.NewGuid();
            var queryFactory = provider.GetRequiredService<IConfigurationQueryFactory>();
            var filePath1 = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _configurationService = provider.GetRequiredService<IConfigurationService2D>();
            _query = queryFactory.CreateConfigurationQuery(filePath1);
            _tileFactory = provider.GetRequiredService<ITileFactory>();
            _positionFactory = provider.GetRequiredService<IPositionFactory>();
            _reader = provider.GetRequiredService<IReader>();
            Name = source.Name;
            Description = source.Description;
            ColumnCount = source.ColumnCount;
            RowCount = source.RowCount;
            _mapDataBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(source.RawData));
            MapObjects = ConvertDataToObjects();
            SaveData();
        }

        private IEnumerable<IMapObject2D> ConvertDataToObjects()
        {
            var id = Guid.NewGuid();
            var tempPath = Path.Join(Path.GetTempPath(), $"{id}.txt");
            File.Create(tempPath).Close();
            File.WriteAllText(tempPath, Encoding.UTF8.GetString(Convert.FromBase64String(_mapDataBase64)));
            using var stream = new StreamReader(tempPath);
            var mapLayout = _reader.ReadAllLines<int>(stream, int.TryParse, ' ').ToList();
            var list = new List<IMapObject2D>();
            for (var i = 0; i < mapLayout.Count; i++)
            {
                var row = mapLayout[i].ToList();
                for (var j = 0; j < row.Count; j++)
                {
                    var value = row[j];
                    var position = _positionFactory.CreatePosition(j, i);
                    if (!Enum.TryParse(value.ToString(), out TileType type)) continue;

                    var tile = type switch
                    {
                        TileType.Ground => _tileFactory.CreateGround(position, _configurationService),
                        TileType.Wall => _tileFactory.CreateWall(position, _configurationService),
                        TileType.Hole => _tileFactory.CreateHole(position, _configurationService),
                        _ => throw new ArgumentException($"Unknown tile type: {value}")
                    };
                    list.Add(tile);
                }
            }
            return list;
        }

        private void SaveData()
        {
            _query.SetAttribute("name", Name);
            _query.SetAttribute("description", Description);
            _query.SetAttribute("row", RowCount);
            _query.SetAttribute("col", ColumnCount);
            _query.SetAttribute("data", _mapDataBase64);
        }
    }
}
