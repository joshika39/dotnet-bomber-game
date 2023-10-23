using Bomber.BL.Entities;
using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using Bomber.UI.Shared.Views;
using GameFramework.Configuration;
using GameFramework.Core.Factories;
using GameFramework.Core.Motion;
using GameFramework.Entities;
using GameFramework.Time;
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
                _configurationService,
                mapLayout.PlayerPosition);

            _configurationService.SetActiveMap(map);
            _configurationService.GameIsRunning = true;
            return map;
        }
        
        public void BombExploded(IBomb bomb, IBomber bomber)
        {
            var map = _configurationService.GetActiveMap<IBomberMap>();
            if (map is null)
            {
                return;
            }

            var affectedObjects = map.MapPortion(bomb.Position, bomb.Radius);

            var entities = map.GetEntitiesAtPortion(affectedObjects);
            foreach (var entity in entities)
            {
                map.Entities.Remove(entity);
                if (entity is IEnemy)
                {
                    bomber.Score += 1;
                }
                entity.Dispose();
            }
        }
        
        public void HandleKeyPress(char keyChar, IBomber bomber)
        {
            var map = _configurationService.GetActiveMap<IBomberMap>();

            switch (keyChar)
            {
                case 'd':
                    map?.MoveUnit(bomber, Move2D.Right);
                    break;
                case 'a':
                    map?.MoveUnit(bomber, Move2D.Left);
                    break;
                case 'w':
                    map?.MoveUnit(bomber, Move2D.Forward);
                    break;
                case 's':
                    map?.MoveUnit(bomber, Move2D.Backward);
                    break;
            }

        }
        
        public void PauseGame(IStopwatch stopwatch)
        {
            if (_configurationService.GameIsRunning)
            {
                stopwatch.Stop();
                _configurationService.GameIsRunning = false;
            }
            else
            {
                stopwatch.Start();
                _configurationService.GameIsRunning = true;
            }
        }
    }
}
