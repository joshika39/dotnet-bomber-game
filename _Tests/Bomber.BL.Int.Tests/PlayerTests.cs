using Bomber.BL.Entities;
using Bomber.BL.Impl.Entities;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Map.MapObject;
using GameFramework.Tiles;
using Infrastructure.Application;
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
                _ = new PlayerModel(view, position, configurationService, name, email, Mock.Of<IGameManager>(),Mock.Of<ILifeCycleManager>());
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
        
        [Fact]
        public void BT_0021_Given_PlayerModel_IsObstacleCalled_Then_ReturnsFalse()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), Mock.Of<IConfigurationService2D>(),"Some Name", "email", Mock.Of<IGameManager>(),Mock.Of<ILifeCycleManager>());
            Assert.NotNull(player);
            Assert.False(player.IsObstacle);
        }
        
        
        [Fact]
        public void BT_0031_Given_PlantedBombs_When_BombExploded_Then_BombWillBeRemovedFromPlantedBombs()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), Mock.Of<IConfigurationService2D>(),"Some Name", "email", Mock.Of<IGameManager>(),Mock.Of<ILifeCycleManager>());
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
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), Mock.Of<IConfigurationService2D>(),"Some Name", "email", GetGameManagerMock().Object ,Mock.Of<ILifeCycleManager>());
            
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
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), Mock.Of<IConfigurationService2D>(),"Some Name", "email", GetGameManagerMock().Object ,Mock.Of<ILifeCycleManager>());
            
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
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), Mock.Of<IConfigurationService2D>(),"Some Name", "email", Mock.Of<IGameManager>(),Mock.Of<ILifeCycleManager>());
       
            player.Kill();
            player.Dispose();
            
            Assert.NotNull(player);
            Assert.False(player.IsObstacle);
        }
        
        [Fact]
        public void BT_0071_Given_Player_When_OnEmailAndNameAndIdGet_Then_ReturnsCorrectValues()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), Mock.Of<IConfigurationService2D>(),"Some Name", "email", Mock.Of<IGameManager>(),Mock.Of<ILifeCycleManager>());
            
            Assert.NotNull(player);
            Assert.Equal("Some Name", player.Name);
            Assert.Equal("email", player.Email);
            Assert.NotEqual(Guid.Empty, player.Id);
        }
        
        // TODO: ConfigService's implementation changed
        [Fact(Skip = "Implement Game manager state check")]
        public void BT_0081_Given_Player_When_SteppedOnEnemy_Then_GameFinished()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(),Mock.Of<IPosition2D>(), Mock.Of<IConfigurationService2D>(),"Some Name", "email", GetGameManagerMock().Object, Mock.Of<ILifeCycleManager>());
       
            player.SteppedOn(Mock.Of<IEnemy>());
        }
        
        [Fact]
        public void BT_0091_Given_Player_When_SteppedOnGround_Then_ChangedPosition()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(), PositionFactory.CreatePosition(0, 0), Mock.Of<IConfigurationService2D>(),"Some Name", "email", Mock.Of<IGameManager>(), Mock.Of<ILifeCycleManager>());
       
            player.Step(GetMapObjectMockObject<IMapObject2D>(1, 0));
            
            Assert.Equal(1, player.Position.X);
            Assert.Equal(0, player.Position.Y);
        }
        
        // TODO: ConfigService's implementation changed
        [Fact(Skip = "Implement Game manager state check")]
        public void BT_0101_Given_Player_When_SteppedOnDeadlyTile_Then_PlayerIsDead()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(), PositionFactory.CreatePosition(0, 0), Mock.Of<IConfigurationService2D>(),"Some Name", "email", Mock.Of<IGameManager>(), Mock.Of<ILifeCycleManager>());
       
            player.Step(GetMapObjectMockObject<IDeadlyTile>(1, 0));
            
            Assert.Equal(1, player.Position.X);
            Assert.Equal(0, player.Position.Y);
        }
        
        // TODO: ConfigService's implementation changed
        [Fact(Skip = "Implement Game manager state check")]
        public void BT_0111_Given_Player_When_GameIsStoppedPlayerStepped_Then_NothingHappens()
        {
            var player = new PlayerModel(Mock.Of<IPlayerView>(), PositionFactory.CreatePosition(0, 0), Mock.Of<IConfigurationService2D>(),"Some Name", "email", Mock.Of<IGameManager>(), Mock.Of<ILifeCycleManager>());
       
            player.Step(GetMapObjectMockObject<IDeadlyTile>(1, 0));
            player.Step(GetMapObjectMockObject<IMapObject2D>(2, 0));
            
            Assert.Equal(1, player.Position.X);
            Assert.Equal(0, player.Position.Y);
        }
        
        [Fact]
        public void BT_0121_Given_Player_When_ViewLoaded_Then_TheViewIsUpdated()
        {
            var viewMock = new Mock<IPlayerView>();
            _ = new PlayerModel(viewMock.Object, PositionFactory.CreatePosition(0, 0), Mock.Of<IConfigurationService2D>(),"Some Name", "email", Mock.Of<IGameManager>(), Mock.Of<ILifeCycleManager>());
            
            viewMock.Object.ViewLoaded();
            viewMock.Verify(p => p.UpdatePosition(It.IsAny<IPosition2D>()), Times.Once);
        }
    }
}
