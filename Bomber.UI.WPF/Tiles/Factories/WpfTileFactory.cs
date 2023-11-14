using System;
using Bomber.BL.Map;
using Bomber.BL.Tiles;
using Bomber.BL.Tiles.Factories;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Map.MapObject;

namespace Bomber.UI.WPF.Tiles.Factories
{
    internal class WpfTileFactory : ITileFactory
    {
        private readonly IConfigurationService2D _configurationService;


        public WpfTileFactory(IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        }
        
        public IPlaceHolder CreatePlaceHolder(IPosition2D position, TileType tileType = TileType.Ground)
        {
            throw new System.NotImplementedException();
        }
        
        public IMapObject2D CreateGround(IPosition2D position)
        {
            return new GroundTile(position, _configurationService);
        }
        
        public IMapObject2D CreateWall(IPosition2D position)
        {
            return new WallTile(position, _configurationService);
        }
        
        public IMapObject2D CreateHole(IPosition2D position)
        {
            return new HoleTile(position, _configurationService);
        }
    }
}
