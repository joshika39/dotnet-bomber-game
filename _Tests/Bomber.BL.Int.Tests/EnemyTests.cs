using Bomber.BL.Impl.Entities;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Map.MapObject;
using Moq;

namespace Bomber.BL.Int.Tests
{
    public class EnemyTests : ABomberTest
    {
        public static IEnumerable<object[]> GetMemberData_0001()
        {
            yield return new[] { (object)null!, Mock.Of<IConfigurationService2D>(), Mock.Of<IPosition2D>() };
            yield return new[] { Mock.Of<IEnemyView>(), (object)null!, Mock.Of<IPosition2D>() };
            yield return new[] { Mock.Of<IEnemyView>(), Mock.Of<IConfigurationService2D>(), (object)null! };
        }

        [Theory]
        [MemberData(nameof(GetMemberData_0001))]
        public void BT_0001_Given_NullArgument_WhenConstructorIsCalled_Then_ThrowsException(
            IEnemyView view,
            IConfigurationService2D configurationService,
            IPosition2D position)
        {
            var exception = Record.Exception(() =>
            {
                _ = new Enemy(view, configurationService, position, CancellationToken.None);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
        
        [Fact]
        public void BT_0021_Given_Enemy_IsObstacleCalled_Then_ReturnsFalse()
        {
            var enemy = new Enemy(Mock.Of<IEnemyView>(), GetConfigurationMock().Object, Mock.Of<IPosition2D>(), CancellationToken.None);
            Assert.NotNull(enemy);
            Assert.False(enemy.IsObstacle);
        }
    }
}
