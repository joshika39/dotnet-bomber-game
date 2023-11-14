using GameFramework.Core.Position;
using GameFramework.Map;

namespace Bomber.BL.Map
{
    public interface IBomberMap : IMap2D<IBomberMapSource, IBomberMapView>
    {
        bool HasEnemy(IPosition2D position);
    }
}
