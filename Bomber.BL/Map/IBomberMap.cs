using Bomber.BL.Entities;
using Bomber.UI.Shared.Entities;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Map
{
    public interface IBomberMap : IMap2D
    {
        IPosition2D PlayerPosition { get; }
        bool HasEnemy(IPosition2D position);
        
        IEnumerable<IMapObject2D> MapPortion(IPosition2D topLeft, IPosition2D bottomRight);
        IEnumerable<IMapObject2D> MapPortion(IPosition2D center, int radius);
        IEnumerable<IBomberEntity> GetEntitiesAtPortion(IEnumerable<IMapObject2D> mapObjects);
        IEnumerable<IBomberEntity> GetEntitiesAtPortion(IPosition2D topLeft, IPosition2D bottomRight);
    }
}
