using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core.Factories;
using GameFramework.Core.Motion;
using GameFramework.Core.Position;
using GameFramework.Entities;
using GameFramework.Impl.Map;

namespace Bomber.BL.Impl.Map
{
    public class BomberMap : AMap2D<IBomberMapSource, IBomberMapView>, IBomberMap
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IEntityViewFactory _entityViewFactory;

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

        public override void MoveUnit(IUnit2D unit2D, Move2D move)
        {
            var mapObject = SimulateMove(unit2D.Position, move);
            if (mapObject is null || mapObject.IsObstacle)
            {
                return;
            }
            var steppedUnits = GetUnitsAtPortion(new[]
            {
                mapObject
            });

            foreach (var steppedUnit in steppedUnits)
            {
                unit2D.SteppedOn(steppedUnit);
            }

            unit2D.Step(mapObject);
        }

        private void FillEntities(IBomberMapSource bomberMapSource)
        {
            foreach (var entity in bomberMapSource.Enemies)
            {
                Units.Add(_entityFactory.CreateEnemy(_entityViewFactory.CreateEnemyView(), PositionFactory.CreatePosition(entity.X, entity.Y)));
            }
        }

        public BomberMap(
            IBomberMapSource mapSource,
            IBomberMapView view,
            IPositionFactory positionFactory,
            IConfigurationService2D configurationService,
            IEntityFactory entityFactory,
            IEntityViewFactory entityViewFactory)
            : base(mapSource, view, positionFactory, configurationService)
        {
            _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
            _entityViewFactory = entityViewFactory ?? throw new ArgumentNullException(nameof(entityViewFactory));

            FillEntities(mapSource);
        }
    }
}
