using Bomber.BL.Impl.Entities;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using Moq;

namespace Bomber.BL.Int.Tests
{
    public class BombTests : ABomberTest

    {
        public static IEnumerable<object[]> GetMemberData_0001()
        {
            yield return new[] { (object)null!, Mock.Of<IPosition2D>(), Mock.Of<IConfigurationService2D>(), Mock.Of<IEnumerable<IBombWatcher>>() };
            yield return new[] { Mock.Of<IBombView>(), (object)null!, Mock.Of<IConfigurationService2D>(), Mock.Of<IEnumerable<IBombWatcher>>() };
            yield return new[] { Mock.Of<IBombView>(), Mock.Of<IPosition2D>(), (object)null!, Mock.Of<IEnumerable<IBombWatcher>>() };
            yield return new[] { Mock.Of<IBombView>(), Mock.Of<IPosition2D>(), Mock.Of<IConfigurationService2D>(), (object)null! };
        }

        [Theory]
        [MemberData(nameof(GetMemberData_0001))]
        public void BT_0001_Given_NullArgument_WhenConstructorIsCalled_Then_ThrowsException(
            IBombView view,
            IPosition2D position,
            IConfigurationService2D configurationService,
            IEnumerable<IBombWatcher> bombWatchers)
        {
            var exception = Record.Exception(() =>
            {
                _ = new Bomb(view, position, configurationService, bombWatchers, 0, CancellationToken.None);
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
                var bomb = new Bomb(Mock.Of<IBombView>(), Mock.Of<IPosition2D>(), Mock.Of<IConfigurationService2D>(), Mock.Of<IEnumerable<IBombWatcher>>(), radius, CancellationToken.None);
                Assert.Null(bomb);
            });

            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Equal("Radius cannot be zero or negative", exception.Message);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(7)]
        public void BT_0011_Given_ValidRadius_WhenConstructorIsCalled_Then_ReturnsCorrectRadius(int radius)
        {
            var bomb = new Bomb(Mock.Of<IBombView>(), Mock.Of<IPosition2D>(), GetConfigurationMock().Object, Mock.Of<IEnumerable<IBombWatcher>>(), radius, CancellationToken.None);

            Assert.NotNull(bomb);
            Assert.Equal(radius, bomb.Radius);
        }

        [Fact]
        public void BT_0021_Given_Bomb_IsObstacleCalled_Then_ReturnsFalse()
        {
            var bomb = new Bomb(Mock.Of<IBombView>(), Mock.Of<IPosition2D>(), GetConfigurationMock().Object, Mock.Of<IEnumerable<IBombWatcher>>(), 5, CancellationToken.None);
            Assert.NotNull(bomb);
            Assert.False(bomb.IsObstacle);
        }
        
        [Fact]
        public async Task BT_0031_Given_Bomb_When_Exploded_Then_WatchersAreNotified()
        {
            var watchers = new List<IBombWatcher>()
            {
                Mock.Of<IBombWatcher>(),
                Mock.Of<IBombWatcher>(),
                Mock.Of<IBombWatcher>()
            };
            
            var bomb = new Bomb(Mock.Of<IBombView>(), Mock.Of<IPosition2D>(), GetConfigurationMock().Object, watchers, 5, CancellationToken.None);
            await bomb.Detonate();
            
            foreach (var watcher in watchers)
            {
                Mock.Get(watcher).Verify(x => x.BombExploded(bomb), Times.Once);
            }
            Assert.Null(bomb);
        }
    }
}
