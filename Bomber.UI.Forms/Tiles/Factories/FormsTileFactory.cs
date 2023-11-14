using Bomber.BL.Map;
using Bomber.BL.Tiles;
using Bomber.BL.Tiles.Factories;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Map.MapObject;

namespace Bomber.UI.Forms.Tiles.Factories
{
    internal class FormsTileFactory : ITileFactory
    {
        private readonly IConfigurationService2D _configurationService2D;
        public FormsTileFactory(IConfigurationService2D configurationService2D)
        {
            _configurationService2D = configurationService2D ?? throw new ArgumentNullException(nameof(configurationService2D));

        }
        
        public IPlaceHolder CreatePlaceHolder(IPosition2D position, TileType tileType = TileType.Ground)
        {
            return new PlaceHolderTile(position, _configurationService2D, tileType);
        }
        
        public IMapObject2D CreateWall(IPosition2D position)
        {
            return new WallTile(position, _configurationService2D);
        }
        
        public IMapObject2D CreateGround(IPosition2D position)
        {
            return new GroundTile(position, _configurationService2D);
        }
        
        public IMapObject2D CreateHole(IPosition2D position)
        {
            return new Hole(position, _configurationService2D);
        }
    }
}
