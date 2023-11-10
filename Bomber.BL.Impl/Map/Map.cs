using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.BL.MapGenerator.DomainModels;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Core.Motion;
using GameFramework.Entities;
using GameFramework.Impl.Map;
using GameFramework.Map;
using GameFramework.Map.MapObject;
using GameFramework.Visuals;

namespace Bomber.BL.Impl.Map
{
    public class Map : AMap2D<IBomberMapSource, IMapView2D>, IBomberMap
    {
        private readonly IPositionFactory _positionFactory;
        private readonly IEntityViewFactory _entityViewFactory;
        private readonly IEntityFactory _entityFactory;
        public bool HasEnemy(IPosition2D position)
        {
            foreach (var entity in Units)
            {
                if (entity is not IEnemy npc)
                {
                    continue;
                }

                if (npc.Position == position)
                {
                    return true;
                }
            }
            return false;
        }

        // private void FillEntities(IMapSource2D bomberMapSource)
        // {
        //     foreach (var entity in bomberMapSource.Units)
        //     {
        //         Units.Add(_entityFactory.CreateEnemy(_entityViewFactory.CreateEnemyView(), _positionFactory.CreatePosition(entity.Position.X, entity.Position.Y)));
        //     }
        // }

        public Map(IBomberMapSource mapSource, IMapView2D view, IPositionFactory positionFactory, IConfigurationService2D configurationService) : base(mapSource, view, positionFactory, configurationService)
        {
            _positionFactory = positionFactory ?? throw new ArgumentNullException(nameof(positionFactory));
        }
    }
}
