using Bomber.BL.Map;
using Bomber.BL.Tiles;
using Bomber.BL.Tiles.Factories;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Map.MapObject;

namespace Bomber.UI.Forms.Tiles.Factories
{
    internal class FormsTileFactory : ITileFactory
    {
        public IPlaceHolder CreatePlaceHolder(IPosition2D position, IConfigurationService2D configurationService, TileType tileType = TileType.Ground)
        {
            return new PlaceHolderTile(position, configurationService, tileType);
        }
        
        public IMapObject2D CreateWall(IPosition2D position, IConfigurationService2D configurationService)
        {
            return new WallTile(position, configurationService);
        }
        
        public IMapObject2D CreateGround(IPosition2D position, IConfigurationService2D configurationService)
        {
            return new GroundTile(position, configurationService);
        }
        
        public IMapObject2D CreateHole(IPosition2D position, IConfigurationService2D configurationService)
        {
            return new Hole(position, configurationService);
        }
    }
}
