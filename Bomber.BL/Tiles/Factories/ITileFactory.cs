using Bomber.BL.Map;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Tiles.Factories
{
    public interface ITileFactory
    {
        IPlaceHolder CreatePlaceHolder(IPosition2D position, TileType tileType = TileType.Ground);
        IMapObject2D CreateGround(IPosition2D position);
        IMapObject2D CreateWall(IPosition2D position);
        IMapObject2D CreateHole(IPosition2D position);
    }
}
