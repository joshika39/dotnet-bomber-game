using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using Bomber.UI.Shared.Views;
using GameFramework.Configuration;
using GameFramework.Core.Factories;
using GameFramework.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.BL.Impl.Models
{
    public class AMainWindowModel : IMainWindowModel
    {
        private readonly IServiceProvider _provider;
        private readonly IPositionFactory _factory;
        private readonly IConfigurationService2D _configurationService;
        
        protected AMainWindowModel(IServiceProvider provider, IConfigurationService2D configurationService)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _factory = _provider.GetRequiredService<IPositionFactory>();
        }

        public IBomberMap OpenMap(string mapFileName)
        {
            var mapLayout = new MapLayout(mapFileName, _provider);
            var map = new Map.Map(
                mapLayout.ColumnCount,
                mapLayout.RowCount,
                new List<IUnit2D>(),
                mapLayout.MapObjects,
                _factory,
                _configurationService);

            _configurationService.SetActiveMap(map);
            _configurationService.GameIsRunning = true;
            return map;
        }
    }
}
