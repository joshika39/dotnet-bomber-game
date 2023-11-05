using System.Diagnostics;
using Bomber.BL.Entities;
using Bomber.BL.Feedback;
using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Core.Motion;
using GameFramework.Entities;
using GameFramework.GameFeedback;
using GameFramework.Impl.Map.Source;
using GameFramework.Visuals;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.BL.Impl.Models
{
    public class AMainWindowModel : ObservableObject, IMainWindowModel, IGameManagerSubscriber
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

        public IBomberMap OpenMap(string mapFileName, IMapView2D mapView2D)
        {
            var source = new BomberMapSource(_provider, mapFileName);
            var map = new Map.Map(source, mapView2D, _factory, _provider.GetRequiredService<IEntityFactory>(), _provider.GetRequiredService<IEntityViewFactory>());

            _gameManager.StartGame(new GameplayFeedback(FeedbackLevel.Info, "Game started!"), map);
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

            var entities = map.GetUnitsAtPortion(affectedObjects);
            foreach (var entity in entities)
            {
                map.Units.Remove(entity);
                if (entity is IEnemy)
                {
                    bomber.Score += 1;
                }
                if(entity is IBomber)
                {
                    _gameManager.EndGame(new GameplayFeedback(FeedbackLevel.Info, "You lost! You got exploded!"), GameResolution.Loss);
                }
                
                entity.Kill();
            }

            if (!map.Units.Any(entity => entity is IEnemy))
            {
                _gameManager.EndGame(new GameplayFeedback(FeedbackLevel.Info, $"You won! The game lasted: {_gameManager.Timer.Elapsed:g}"), GameResolution.Win);
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
            
            if (int.TryParse(keyChar.ToString(), out var bombIndex))
            {
                bomber.DetonateBombAt(bombIndex - 1);
            }

        }
        
        public void PutBomb()
        {
            
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
            
            foreach (var unit in map.Units)
            {
                // TODO: Uncomment when update is released to the game framework 
                // unit.Dispose();
            }
        }
        
        public void OnGamePaused()
        {
            Debug.WriteLine("Game paused");
        }
        public void OnGameResumed()
        {
            throw new NotImplementedException();
        }
        public void OnGameReset()
        {
            throw new NotImplementedException();
        }

        public void PauseGame()
        {
            if (_gameManager.State == GameState.Paused)
            {
                _gameManager.ResumeGame();
            }
            else
            {
                _gameManager.PauseGame();
            }
        }
    }
}
