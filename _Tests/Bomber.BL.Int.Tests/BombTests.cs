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
    public class BombTests : ABomberTest

    {
        public static IEnumerable<object[]> GetMemberData_0001()
        {
            yield return new[]
            {
                (object)null!,
                Mock.Of<IPosition2D>(),
                Mock.Of<ILifeCycleManager>()
            };
            yield return new[]
            {
                Mock.Of<IBombView>(),
                (object)null!,
                Mock.Of<ILifeCycleManager>()
            };
            yield return new[]
            {
                Mock.Of<IBombView>(),
                Mock.Of<IPosition2D>(),
                (object)null!
            };
        }
        [Theory]
        [MemberData(nameof(GetMemberData_0001))]
        public void BT_0001_Given_NullArgument_WhenConstructorIsCalled_Then_ThrowsException(
            IBombView view,
            IPosition2D position,
            ILifeCycleManager lifeCycleManager)
        {
            var exception = Record.Exception(() =>
            {
                _ = new Bomb(view, position, 0, GetGameManagerMock().Object, lifeCycleManager, Mock.Of<IBoardService>());
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-3)]
        public void BT_0011_Given_ZeroOrNegativeRadius_WhenConstructorIsCalled_Then_ThrowsException(int radius)
        {
            var exception = Record.Exception(() =>
            {
                var bomb = new Bomb(Mock.Of<IBombView>(), Mock.Of<IPosition2D>(), radius, GetGameManagerMock().Object, Mock.Of<ILifeCycleManager>(), Mock.Of<IBoardService>());
                Assert.Null(bomb);
            });

            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Equal("Radius cannot be zero or negative", exception.Message);
        }

        [Theory(Skip = "Needs refactoring")]
        [InlineData(3)]
        [InlineData(7)]
        public void BT_0011_Given_ValidRadius_WhenConstructorIsCalled_Then_ReturnsCorrectRadius(int radius)
        {
            var bomb = new Bomb(Mock.Of<IBombView>(), Mock.Of<IPosition2D>(), radius, GetGameManagerMock().Object, Mock.Of<ILifeCycleManager>(), Mock.Of<IBoardService>());

            Assert.NotNull(bomb);
            Assert.Equal(radius, bomb.Radius);
        }

        [Fact(Skip = "Needs refactoring")]
        public void BT_0021_Given_Bomb_IsObstacleCalled_Then_ReturnsFalse()
        {
            var bomb = new Bomb(Mock.Of<IBombView>(), Mock.Of<IPosition2D>(), 5, GetGameManagerMock().Object, Mock.Of<ILifeCycleManager>(), Mock.Of<IBoardService>());
            Assert.NotNull(bomb);
            Assert.False(bomb.IsObstacle);
        }

        [Fact(Skip = "Needs refactoring")]
        public async Task BT_0031_Given_Bomb_When_Exploded_Then_WatchersAreNotified()
        {
            var watchers = new List<IBombWatcher>()
            {
                Mock.Of<IBombWatcher>(),
                Mock.Of<IBombWatcher>(),
                Mock.Of<IBombWatcher>()
            };
            var view = Mock.Of<IBombView>();
            var bomb = new Bomb(view, Mock.Of<IPosition2D>(), 5, GetGameManagerMock().Object, Mock.Of<ILifeCycleManager>(), Mock.Of<IBoardService>());
            bomb.Attach(watchers[0]);
            bomb.Attach(watchers[1]);
            bomb.Attach(watchers[2]);
            await bomb.Detonate();
            await bomb.Detonate();
            bomb.Dispose();

            foreach (var watcher in watchers)
            {
                Mock.Get(watcher).Verify(x => x.BombExploded(bomb), Times.Once);
            }
            Mock.Get(view).Verify(b => b.Dispose(), Times.Once);
        }
    }
}
