using Bomber.BL.Map;
using GameFramework.Core;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Tiles
{
    public interface IPlaceHolder
    {
        TileType Type { get; }
        IPosition2D Position { get; }
    }
}
