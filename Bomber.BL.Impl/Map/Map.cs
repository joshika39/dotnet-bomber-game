using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Entities;
using GameFramework.Impl.Map;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Map
{
    public class Map : AMap2D, IBomberMap
    {
        private readonly IPositionFactory _positionFactory;

        public Map(int sizeX, int sizeY, ICollection<IUnit2D> entities, IEnumerable<IMapObject2D> mapObjects, IPositionFactory positionFactory, IConfigurationService2D configurationService, IPosition2D playerPosition) : base(sizeX, sizeY, entities, mapObjects)
        {
            _positionFactory = positionFactory ?? throw new ArgumentNullException(nameof(positionFactory));
            PlayerPosition = playerPosition ?? throw new ArgumentNullException(nameof(playerPosition));
        }

        public IPosition2D PlayerPosition { get; }
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
    }
}
