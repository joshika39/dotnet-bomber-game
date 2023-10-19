using System.Reflection;
using Bomber.BL.Entities;
using Bomber.BL.Impl.Entities;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using Moq;

namespace Bomber.BL.Int.Tests
{
    public class PlayerTests : ABomberTest
    {
        public static IEnumerable<object[]> GetMemberData_0001()
        {
            yield return new[] { (object)null!, Mock.Of<IConfigurationService2D>(), Mock.Of<IPosition2D>(), "John Doe", "ex@mail.com" };
            yield return new[] { Mock.Of<IPlayerView>(), (object)null!, Mock.Of<IPosition2D>(), "John Doe", "ex@mail.com"  };
            yield return new[] { Mock.Of<IPlayerView>(), Mock.Of<IConfigurationService2D>(), (object)null!, "John Doe", "ex@mail.com"  };
            yield return new[] { Mock.Of<IPlayerView>(), Mock.Of<IConfigurationService2D>(), Mock.Of<IPosition2D>(), (object)null!, "ex@mail.com"  };
            yield return new[] { Mock.Of<IPlayerView>(), Mock.Of<IConfigurationService2D>(), Mock.Of<IPosition2D>(), "John Doe", (object)null!  };
        }

        [Theory]
        [MemberData(nameof(GetMemberData_0001))]
        public void BT_0001_Given_NullArgument_WhenConstructorIsCalled_Then_ThrowsException(
            IPlayerView view,
            IConfigurationService2D configurationService,
            IPosition2D position,
            string name,
            string email)
        {
            var exception = Record.Exception(() =>
            {
                _ = new PlayerModel(view, position, configurationService, name, email, CancellationToken.None);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
        
        [Fact]
        public void BT_0021_Given_PlayerModel_IsObstacleCalled_Then_ReturnsFalse()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), GetConfigurationMock().Object,"Some Name", "email", CancellationToken.None);
            Assert.NotNull(player);
            Assert.False(player.IsObstacle);
        }
        
        
        [Fact]
        public void BT_0031_Given_PlantedBombs_When_BombExploded_Then_BombWillBeRemovedFromPlantedBombs()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), GetConfigurationMock().Object,"Some Name", "email", CancellationToken.None);
            Assert.NotNull(player);
            Assert.False(player.IsObstacle);
            var bombMocks = new List<Mock<IBomb>>()
            {
                GetBombMock(player),
                GetBombMock(player),
                GetBombMock(player),
            };
            
            foreach (var bomb in bombMocks)
            {
                player.PlantedBombs.Add(bomb.Object);
            }
            
            for (var i = 0; i < player.PlantedBombs.Count; i++)
            {
                var bomb = player.PlantedBombs.ElementAt(i);
                bomb.Detonate();
                i--;
            }
           
            foreach (var mock in bombMocks)
            {
                mock.Verify(m => m.Detonate(), Times.Once);
            }
            
            Assert.Empty(player.PlantedBombs);
        }
        
        [Fact]
        public void BT_0041_Given_Player_When_PutBombCalled_Then_PlantedBombsWillIncrease()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), GetConfigurationMock().Object,"Some Name", "email", CancellationToken.None);
            
            for (var i = 0; i < 3; i++)
            {
                player.PutBomb(Mock.Of<IBombView>(), default(IBombWatcher));
            }
            
            Assert.NotNull(player);
            Assert.False(player.IsObstacle);
            Assert.Equal(3, player.PlantedBombs.Count);
        }
        
        [Fact]
        public void BT_0051_Given_Player_When_Killed_Then_PlayerBombsWillBeRemoved()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), GetConfigurationMock().Object,"Some Name", "email", CancellationToken.None);
            
            for (var i = 0; i < 3; i++)
            {
                player.PutBomb(Mock.Of<IBombView>(), default(IBombWatcher));
            }
            
            player.Kill();
            
            Assert.NotNull(player);
            Assert.False(player.IsObstacle);
            Assert.Empty(player.PlantedBombs);
        }
        
        [Fact]
        public void BT_0061_Given_Player_When_Killed_Then_Disposes()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), GetConfigurationMock().Object,"Some Name", "email", CancellationToken.None);
       
            player.Kill();
            player.Dispose();
            
            Assert.NotNull(player);
            Assert.False(player.IsObstacle);
        }
        
        [Fact]
        public void BT_0071_Given_Player_When_OnEmailAndNameAndIdGet_Then_ReturnsCorrectValues()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), GetConfigurationMock().Object,"Some Name", "email", CancellationToken.None);
            
            Assert.NotNull(player);
            Assert.Equal("Some Name", player.Name);
            Assert.Equal("email", player.Email);
            Assert.NotEqual(Guid.Empty, player.Id);
        }
    }
}
