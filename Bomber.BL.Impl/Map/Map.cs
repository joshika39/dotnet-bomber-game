using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.BL.MapGenerator.DomainModels;
using Bomber.UI.Shared.Entities;
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
    public class Map : AMap2D, IBomberMap
    {
        private readonly IPositionFactory _positionFactory;
        private readonly IEntityViewFactory _entityViewFactory;
        private readonly IEntityFactory _entityFactory;

        public IBomberMapSource BomberMapSource { get; }

        public Map(IBomberMapSource mapSource, IMapView2D view, IPositionFactory positionFactory, IEntityFactory entityFactory, IEntityViewFactory entityViewFactory) : base(mapSource, view, positionFactory)
        {
            BomberMapSource = mapSource ?? throw new ArgumentException("Map source is not a BomberMapSource");
            _positionFactory = positionFactory ?? throw new ArgumentNullException(nameof(positionFactory));
            _entityViewFactory = entityViewFactory ?? throw new ArgumentNullException(nameof(entityViewFactory));
            _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
            FillEntities(mapSource);
        }

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

        private void FillEntities(IMapSource2D bomberMapSource)
        {
            foreach (var entity in bomberMapSource.Units)
            {
                Units.Add(_entityFactory.CreateEnemy(_entityViewFactory.CreateEnemyView(), _positionFactory.CreatePosition(entity.Position.X, entity.Position.Y)));
            }
        }
        
    }
}
