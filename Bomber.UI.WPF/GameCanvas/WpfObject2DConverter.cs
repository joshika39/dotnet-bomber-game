using System;
using Bomber.UI.WPF.Tiles;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Map.MapObject;

namespace Bomber.UI.WPF.GameCanvas
{
    internal class WpfObject2DConverter : IMapObject2DConverter
    {
        private readonly IConfigurationService2D _configurationService2D;

        public WpfObject2DConverter(IConfigurationService2D configurationService2D)
        {
            _configurationService2D = configurationService2D ?? throw new ArgumentNullException(nameof(configurationService2D));
        }

        public IMapObject2D FromEnum<T>(T tileType, IPosition2D position) where T : Enum
        {
            if (tileType is not TileType)
            {
                throw new ArgumentException($"tileType must be of type {nameof(TileType)}");
            }
            
            return tileType switch
            {
                TileType.Ground => new GroundTile(position, _configurationService2D),
                TileType.Wall => new WallTile(position, _configurationService2D),
                TileType.Hole => new HoleTile(position, _configurationService2D),
                _ => throw new ArgumentException($"Unknown tile type: {tileType}")
            };
        }

        public IMapObject2D FromInt(int type, IPosition2D position)
        {
            throw new NotImplementedException();
        }
    }
}
