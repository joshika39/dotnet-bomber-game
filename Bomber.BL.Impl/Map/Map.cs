using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.BL.MapGenerator.DomainModels;
using Bomber.UI.Shared.Entities;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Core.Motion;
using GameFramework.Entities;
using GameFramework.Impl.Map;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Map
{
    public class Map : AMap2D, IBomberMap
    {
        private readonly IPositionFactory _positionFactory;
        private readonly IEntityViewFactory _entityViewFactory;
        private readonly IEntityFactory _entityFactory;

        public IMapLayout MapLayout { get; }

        public Map(int sizeX, int sizeY, ICollection<IUnit2D> entities, IEnumerable<IMapObject2D> mapObjects, IPositionFactory positionFactory, IEntityViewFactory entityViewFactory, IEntityFactory entityFactory, IMapLayout mapLayout) : base(sizeX, sizeY, entities, mapObjects)
        {
            MapLayout = mapLayout ?? throw new ArgumentNullException(nameof(mapLayout));
            _positionFactory = positionFactory ?? throw new ArgumentNullException(nameof(positionFactory));
            _entityViewFactory = entityViewFactory ?? throw new ArgumentNullException(nameof(entityViewFactory));
            _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
            FillEntities(mapLayout);
        }

        public override void MoveUnit(IUnit2D unit2D, Move2D move)
        {
            base.MoveUnit(unit2D, move);
            foreach (var entity in Entities)
            {
                if (!entity.Equals(unit2D) && entity.Position.Equals(unit2D.Position))
                {
                    entity.SteppedOn(unit2D);
                }
            }
        }

        public bool HasEnemy(IPosition2D position)
        {
            foreach (var entity in Entities)
            {
                if (entity is not INpc npc)
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

        public IEnumerable<IBomberEntity> GetEntitiesAtPortion(IEnumerable<IMapObject2D> mapObjects)
        {
            var entities = new List<IBomberEntity>();
            foreach (var mapObject in mapObjects)
            {
                foreach (var entity in Entities)
                {
                    if (entity is IBomberEntity bomberEntity && entity.Position == mapObject.Position)
                    {
                        entities.Add(bomberEntity);
                    }
                }
            }

            return entities;
        }

        public IEnumerable<IBomberEntity> GetEntitiesAtPortion(IPosition2D topLeft, IPosition2D bottomRight)
        {
            var objects = MapPortion(topLeft, bottomRight);
            return GetEntitiesAtPortion(objects);
        }
        
        public void SaveProgress(IBomber bomber)
        {
            MapLayout.SaveLayout(bomber, Entities.Where(e => e is INpc).Select(e => new DummyEntity(e.Position.X, e.Position.Y)).ToList());
        }

        public IEnumerable<IMapObject2D> MapPortion(IPosition2D topLeft, IPosition2D bottomRight)
        {
            var objects = MapObjects.ToArray();
            for (var y = topLeft.Y; y <= bottomRight.Y; y++)
            {
                for (var x = topLeft.X; x <= bottomRight.X; x++)
                {
                    yield return objects[y * SizeX + x];
                }
            }
        }

        public IEnumerable<IMapObject2D> MapPortion(IPosition2D center, int radius)
        {
            var top = center.Y - radius < 0 ? 0 : center.Y - radius;
            var bottom = center.Y + radius >= SizeY ? SizeY - 1 : center.Y + radius;
            var left = center.X - radius < 0 ? 0 : center.X - radius;
            var right = center.X + radius >= SizeX ? SizeX - 1 : center.X + radius;
            var topLeftPos = _positionFactory.CreatePosition(left, top);
            var bottomRightPos = _positionFactory.CreatePosition(right, bottom);
            return MapPortion(topLeftPos, bottomRightPos);
        }

        private void FillEntities(IMapLayout mapLayout)
        {
            foreach (var entity in mapLayout.Entities)
            {
                Entities.Add(_entityFactory.CreateEnemy(_entityViewFactory.CreateEnemyView(), _positionFactory.CreatePosition(entity.X, entity.Y)));
            }
        }
    }
}
