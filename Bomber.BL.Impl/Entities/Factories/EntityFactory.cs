using Bomber.BL.Entities;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using Infrastructure.Application;

namespace Bomber.BL.Impl.Entities.Factories
{
    internal class EntityFactory : IEntityFactory
    {
        private readonly IConfigurationService2D _configurationService;
        private readonly IGameManager _gameManager;
        private readonly ILifeCycleManager _lifeCycleManager;
        public EntityFactory(IConfigurationService2D configurationService, IGameManager gameManager, ILifeCycleManager lifeCycleManager)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
            _lifeCycleManager = lifeCycleManager ?? throw new ArgumentNullException(nameof(lifeCycleManager));
        }

        public IBomb CreateBomb(IBombView view, IPosition2D position2D, IEnumerable<IBombWatcher> bombWatchers, int radius)
        {
            return new Bomb(view, position2D, bombWatchers, radius, _gameManager, _lifeCycleManager);
        }
        
        public IBomber CreatePlayer(IPlayerView view, IPosition2D position, string name, string email)
        {
            return new PlayerModel(view, position, _configurationService, name, email, _gameManager, _lifeCycleManager);
        }
        
        public IEnemy CreateEnemy(IEnemyView view, IPosition2D position2D)
        {
            return new Enemy(view, _configurationService, position2D, _gameManager, _lifeCycleManager);
        }
    }
}
