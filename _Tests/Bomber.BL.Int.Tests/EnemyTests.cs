using Bomber.BL.Impl.Entities;
using Bomber.UI.Shared.Entities;
using GameFramework.Board;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;
using Infrastructure.Application;
using Moq;

namespace Bomber.BL.Int.Tests
{
    public class EnemyTests : ABomberTest
    {
        public static IEnumerable<object[]> GetMemberData_0001()
        {
            yield return new[]
            {
                (object)null!,
                Mock.Of<IConfigurationService2D>(),
                Mock.Of<IPosition2D>(),
                Mock.Of<IGameManager>(),
                Mock.Of<ILifeCycleManager>()
            };
            yield return new[]
            {
                Mock.Of<IEnemyView>(),
                (object)null!,
                Mock.Of<IPosition2D>(),
                Mock.Of<IGameManager>(),
                Mock.Of<ILifeCycleManager>()
            };
            yield return new[]
            {
                Mock.Of<IEnemyView>(),
                Mock.Of<IConfigurationService2D>(),
                (object)null!,
                Mock.Of<IGameManager>(),
                Mock.Of<ILifeCycleManager>()
            };
            yield return new[]
            {
                Mock.Of<IEnemyView>(),
                Mock.Of<IConfigurationService2D>(),
                Mock.Of<IPosition2D>(),
                (object)null!,
                Mock.Of<ILifeCycleManager>()
            };
            yield return new[]
            {
                Mock.Of<IEnemyView>(),
                Mock.Of<IConfigurationService2D>(),
                Mock.Of<IPosition2D>(),
                Mock.Of<IGameManager>(),
                (object)null!
            };
        }
        [Theory]
        [MemberData(nameof(GetMemberData_0001))]
        public void BT_0001_Given_NullArgument_WhenConstructorIsCalled_Then_ThrowsException(
            IEnemyView view,
            IBoardService configurationService,
            IPosition2D position,
            IGameManager gameManager,
            ILifeCycleManager lifeCycleManager)
        {
            var exception = Record.Exception(() =>
            {
                _ = new Enemy(view, configurationService, position, gameManager, lifeCycleManager);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void BT_0021_Given_Enemy_IsObstacleCalled_Then_ReturnsFalse()
        {
            var enemy = new Enemy(Mock.Of<IEnemyView>(), Mock.Of<IBoardService>(), Mock.Of<IPosition2D>(), Mock.Of<IGameManager>(), Mock.Of<ILifeCycleManager>());
            Assert.NotNull(enemy);
            Assert.False(enemy.IsObstacle);
        }
    }
}
