using Bomber.BL.Entities;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Time;

namespace Bomber.BL.Impl.Entities.Factories
{
    internal class EntityFactory : IEntityFactory
    {
        private readonly IConfigurationService2D _configurationService;
        private readonly IGameManager _gameManager;
        public EntityFactory(IConfigurationService2D configurationService, IGameManager gameManager)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }

        public IBomb CreateBomb(IBombView view, IPosition2D position2D, IEnumerable<IBombWatcher> bombWatchers, int radius)
        {
            return new Bomb(view, position2D, _configurationService, bombWatchers, radius, _gameManager.Timer);
        }
        
        public IBomber CreatePlayer(IPlayerView view, IPosition2D position, string name, string email)
        {
            return new PlayerModel(view, position, _configurationService, name, email, _gameManager);
        }
        
        public IEnemy CreateEnemy(IEnemyView view, IPosition2D position2D)
        {
            return new Enemy(view, _configurationService, position2D, _gameManager);
        }
    }
}
