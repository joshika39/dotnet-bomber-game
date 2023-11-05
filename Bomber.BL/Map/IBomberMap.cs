using GameFramework.Core;
using GameFramework.Map;

namespace Bomber.BL.Map
{
    public interface IBomberMap : IMap2D
    {
        IBomberMapSource BomberMapSource { get; }
        bool HasEnemy(IPosition2D position);
    }
}
