using System;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;

namespace Bomber.UI.WPF.Entities
{
    internal class WpfEntityFactory : IEntityViewFactory
    {
        private readonly IConfigurationService2D _configurationService;
        public WpfEntityFactory(IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        }
        
        public IEnemyView CreateEnemyView()
        {
            return new EnemyControl(_configurationService);
        }
        
        public IBombView CreateBombView()
        {
            return new BombControl(_configurationService);
        }
    }
}
