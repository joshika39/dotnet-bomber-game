using GameFramework.Core;
using GameFramework.Map;
using GameFramework.Visuals;

namespace Bomber.BL.Map
{
    public interface IBomberMap : IMap2D<IBomberMapSource, IMapView2D>
    {
        bool HasEnemy(IPosition2D position);
    }
}
