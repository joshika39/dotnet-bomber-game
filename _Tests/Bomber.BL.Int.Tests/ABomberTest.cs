using Bomber.BL.Entities;
using Bomber.BL.Map;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Map.MapObject;
using Moq;

namespace Bomber.BL.Int.Tests
{
    public abstract class ABomberTest
    {
        protected Mock<IConfigurationService2D> GetConfigurationMock()
        {
            var configMock = new Mock<IConfigurationService2D>();
            configMock.Setup(c => c.GetActiveMap<IBomberMap>()).Returns(GetMapMock().Object);
            configMock.Setup(c => c.GameIsRunning).Returns(true);
            return configMock;
        }

        protected Mock<IBomberMap> GetMapMock()
        {
            var mapMock = new Mock<IBomberMap>();
            mapMock.Setup(m => m.MapPortion(It.IsAny<IPosition2D>(), It.IsAny<int>())).Returns(Mock.Of<IEnumerable<IMapObject2D>>());
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
    }
}
