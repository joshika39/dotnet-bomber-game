using Bomber.BL.Map;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Tiles.Factories
{
    public interface ITileFactory
    {
        IPlaceHolder CreatePlaceHolder(IPosition2D position, IConfigurationService2D configurationService, TileType tileType = TileType.Ground);
        IMapObject2D CreateGround(IPosition2D position, IConfigurationService2D configurationService);
        IMapObject2D CreateWall(IPosition2D position, IConfigurationService2D configurationService);
        IMapObject2D CreateHole(IPosition2D position, IConfigurationService2D configurationService);
    }
}
