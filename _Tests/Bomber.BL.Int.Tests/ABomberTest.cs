using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.UI.Shared.Views;
using GameFramework.Board;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Impl.Core;
using GameFramework.Map.MapObject;
using GameFramework.Visuals;
using Infrastructure.Time;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Bomber.BL.Int.Tests
{
    public abstract class ABomberTest
    {
        protected IPositionFactory PositionFactory { get; }
        protected ABomberTest()
        {
            var collection = new ServiceCollection();
            new GameFrameworkCore(collection, new CancellationTokenSource()).RegisterServices(collection);
            var provider = collection.BuildServiceProvider();
            PositionFactory = provider.GetRequiredService<IPositionFactory>();
        }
        
        protected static Mock<IBoardService> GetBoardServiceMock()
        {
            var configMock = new Mock<IBoardService>();
            configMock.Setup(c => c.GetActiveMap<IBomberMap, IBomberMapSource, IMapView2D>()).Returns(GetMapMock().Object);
            return configMock;
        }

        private static Mock<IBomberMap> GetMapMock()
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

        private IEnumerable<IBomb> GetBombs(IBombWatcher bombWatcher)
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

        private static Mock<IBomb> GetBombMock(IEnumerable<IBombWatcher> bombWatchers)
        {
            var bombMock = new Mock<IBomb>();
            bombMock.Setup(b => b.Detonate()).Callback(() => BombExploded(bombWatchers, bombMock));
            return bombMock;
        }

        private static void BombExploded(IEnumerable<IBombWatcher> bombWatchers, Mock<IBomb> bombMock)
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

        protected Mock<IGameManager> GetGameManagerMock()
        {
            var gameManagerMock = new Mock<IGameManager>();
            gameManagerMock.Setup(g => g.Timer).Returns(Mock.Of<IStopwatch>());
            return gameManagerMock;
        }
        
        protected static IMapObject2D GetMapObjectMockObject<T>(int x, int y) where T : class, IMapObject2D
        {
            var collection = new ServiceCollection();
            new GameFrameworkCore(collection, new CancellationTokenSource()).RegisterServices(collection);
            var positionFactory = collection.BuildServiceProvider().GetRequiredService<IPositionFactory>();
            var mapObjectMock = new Mock<T>();
            mapObjectMock.Setup(p => p.Position).Returns(positionFactory.CreatePosition(x, y));
            return mapObjectMock.Object;
        }
    } 
}
