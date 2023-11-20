using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;

namespace Bomber.UI.Forms.Views.Entities
{
    internal class EntityViewFactory : IEntityViewFactory
    {
        private readonly IConfigurationService2D _configurationService;

        public EntityViewFactory(IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));

        }
        
        public IEnemyView CreateEnemyView()
        {
            return new EnemyView(_configurationService);
        }
        
        public IBombView CreateBombView()
        {
            return new BombView(_configurationService);
        }
        
        public IPlayerView CreatePlayerView()
        {
            return new PlayerView(_configurationService);
        }
    }
}
