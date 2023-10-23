using System.Diagnostics;
using Bomber.BL.Entities;
using Bomber.BL.Feedback;
using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Core.Motion;
using GameFramework.Entities;
using GameFramework.GameFeedback;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.BL.Impl.Models
{
    public class AMainWindowModel : IMainWindowModel, IGameManagerSubscriber
    {
        private readonly IServiceProvider _provider;
        private readonly IPositionFactory _factory;
        private readonly IConfigurationService2D _configurationService;
        private readonly IGameManager _gameManager;

        protected AMainWindowModel(IServiceProvider provider, IConfigurationService2D configurationService, IGameManager gameManager)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
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
                _provider.GetRequiredService<IEntityViewFactory>(),
                _provider.GetRequiredService<IEntityFactory>(),
                mapLayout);

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

            if (!map.Entities.Any(entity => entity is IEnemy))
            {
                _gameManager.GameFinished(new GameplayFeedback(FeedbackLevel.Info, $"You won! The game lasted: {_gameManager.Timer.Elapsed:g}"), GameResolution.Win);
                _gameManager.Timer.Reset();
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
        
        public void OnGameStarted(IGameplayFeedback feedback)
        {
            Debug.WriteLine("Game started");
        }
        
        public void OnGameFinished(IGameplayFeedback feedback, GameResolution resolution)
        {
            var map = _configurationService.GetActiveMap<IBomberMap>();
            if (map is null)
            {
                return;
            }
            
            foreach (var unit in map.Entities)
            {
                if (unit is IBomberEntity bomberEntity)
                {
                    bomberEntity.Dispose();
                }
            }
        }
        
        public void OnGamePaused()
        {
            Debug.WriteLine("Game paused");
        }
        
        public void PauseGame()
        {
            if (_configurationService.GameIsRunning)
            {
                _gameManager.Timer.Stop();
                _configurationService.GameIsRunning = false;
            }
            else
            {
                _gameManager.Timer.Start();
                _configurationService.GameIsRunning = true;
            }
        }
    }
}
