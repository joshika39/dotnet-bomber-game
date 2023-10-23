using Bomber.BL.Entities;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Time;

namespace Bomber.BL.Impl.Entities.Factories
{
    public class EntityFactory : IEntityFactory
    {
        private readonly IConfigurationService2D _configurationService;
        private readonly IStopwatch _stopwatch;
        public EntityFactory(IConfigurationService2D configurationService, IStopwatch stopwatch)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _stopwatch = stopwatch ?? throw new ArgumentNullException(nameof(stopwatch));
        }

        public IBomb CreateBomb(IBombView view, IPosition2D position2D, IEnumerable<IBombWatcher> bombWatchers, int radius)
        {
            return new Bomb(view, position2D, _configurationService, bombWatchers, radius, _stopwatch);
        }
        
        public IBomber CreatePlayer(IPlayerView view, IPosition2D position, string name, string email)
        {
            return new PlayerModel(view, position, _configurationService, name, email, _stopwatch);
        }
        
        public IEnemy CreateEnemy(IEnemyView view, IPosition2D position2D)
        {
            return new Enemy(view, _configurationService, position2D, _stopwatch);
        }
    }
}
