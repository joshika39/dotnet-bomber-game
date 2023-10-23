using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.UI.Shared.Views;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Impl.Core;
using GameFramework.Map.MapObject;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Bomber.BL.Int.Tests
{
    public abstract class ABomberTest
    {
        private readonly ServiceProvider _provider;
        protected IPositionFactory PositionFactory { get; }
        protected ABomberTest()
        {
            var collection = new ServiceCollection();
            new GameModule().LoadModules(collection, new CancellationTokenSource());
            _provider = collection.BuildServiceProvider();
            PositionFactory = _provider.GetRequiredService<IPositionFactory>();
        }
        
        protected static Mock<IConfigurationService2D> GetConfigurationMock()
        {
            var configMock = new Mock<IConfigurationService2D>();
            configMock.Setup(c => c.GetActiveMap<IBomberMap>()).Returns(GetMapMock().Object);
            configMock.Setup(c => c.CancellationTokenSource).Returns(new CancellationTokenSource());
            configMock.Setup(c => c.GameIsRunning).Returns(true);
            return configMock;
        }
        
        protected Mock<IConfigurationService2D> GetConfigurationMock(bool initValue)
        {
            var configMock = new Mock<IConfigurationService2D>();
            configMock.Setup(c => c.GetActiveMap<IBomberMap>()).Returns(GetMapMock().Object);
            configMock.Setup(c => c.CancellationTokenSource).Returns(new CancellationTokenSource());
            configMock.SetupProperty(v => v.GameIsRunning, initValue);
            return configMock;
        }

        protected static Mock<IBomberMap> GetMapMock()
        {
            var mapMock = new Mock<IBomberMap>();
            var mapObjects = new List<IMapObject2D>();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++) 
                {
                    mapObjects.Add(GetMapObjectMockObject<IBomberMapTileView>(i, j));
                }
            }
            mapMock.Setup(m => m.MapObjects).Returns(mapObjects);
            mapMock.Setup(m => m.MapPortion(It.IsAny<IPosition2D>(), It.IsAny<int>())).Returns(mapObjects);
            return mapMock;
        }

        protected IEnumerable<IBomb> GetBombs(IBombWatcher bombWatcher)
        {
            return new List<IBomb>()
            {
                GetBombMock(bombWatcher).Object,
                GetBombMock(bombWatcher).Object,
                GetBombMock(bombWatcher).Object
            };
        }

        protected Mock<IBomb> GetBombMock(IBombWatcher bombWatcher)
        {
            return GetBombMock(new List<IBombWatcher>() { bombWatcher });
        }

        protected Mock<IBomb> GetBombMock(IEnumerable<IBombWatcher> bombWatchers)
        {
            var bombMock = new Mock<IBomb>();
            bombMock.Setup(b => b.Detonate()).Callback(() => BombExploded(bombWatchers, bombMock));
            return bombMock;
        }

        private void BombExploded(IEnumerable<IBombWatcher> bombWatchers, Mock<IBomb> bombMock)
        {
            foreach (var bombWatcher in bombWatchers)
            {
                bombWatcher.BombExploded(bombMock.Object);
            }
        }

        protected Mock<IBomber> GetPlayerMock()
        {
            var playerMock = new Mock<IBomber>();
            playerMock.Setup(p => p.PlantedBombs).Returns(GetBombs(playerMock.Object).ToList);
            playerMock.Setup(p => p.Kill()).Callback(() => playerMock.Object.Dispose());
            return playerMock;
        }
        
        protected static IMapObject2D GetMapObjectMockObject<T>(int x, int y) where T : class, IMapObject2D
        {
            var collection = new ServiceCollection();
            new GameModule().LoadModules(collection, new CancellationTokenSource());
            var positionFactory = collection.BuildServiceProvider().GetRequiredService<IPositionFactory>();
            var mapObjectMock = new Mock<T>();
            mapObjectMock.Setup(p => p.Position).Returns(positionFactory.CreatePosition(x, y));
            return mapObjectMock.Object;
        }
    }
}
