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
    }
}
