using System.Diagnostics;
using Bomber.BL.Entities;
using Bomber.BL.Feedback;
using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using CommunityToolkit.Mvvm.ComponentModel;
using GameFramework.Board;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Core.Motion;
using GameFramework.GameFeedback;
using GameFramework.Manager;
using GameFramework.Visuals;
using Infrastructure.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.BL.Impl.Models
{
    public class AMainWindowModel : ObservableObject, IMainWindowModel, IRunningGameListener
    {
        protected readonly IServiceProvider Provider;
        protected readonly IPositionFactory PositionFactory;
        protected readonly IConfigurationService2D ConfigurationService;
        protected readonly IGameManager GameManager;
        protected readonly IBoardService BoardService;
        protected readonly ILifeCycleManager LifeCycleManager;

        protected AMainWindowModel(IServiceProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            ConfigurationService = Provider.GetRequiredService<IConfigurationService2D>();
            GameManager = Provider.GetRequiredService<IGameManager>();
            BoardService = Provider.GetRequiredService<IBoardService>();
            PositionFactory = Provider.GetRequiredService<IPositionFactory>();
            LifeCycleManager = Provider.GetRequiredService<ILifeCycleManager>();
        }

        public IBomberMap OpenMap(string mapFileName, IMapView2D mapView2D)
        {
            var source = new BomberMapSource(Provider, mapFileName);
            var map = new Map.Map(source, mapView2D, PositionFactory, ConfigurationService);

            GameManager.StartGame(new GameplayFeedback(FeedbackLevel.Info, "Game started!"));
            BoardService.SetActiveMap<IBomberMap, IBomberMapSource, IMapView2D>(map);
            return map;
        }
        
        public void BombExploded(IBomb bomb, IBomber bomber)
        {
            var map = BoardService.GetActiveMap<IBomberMap, IBomberMapSource, IMapView2D>();
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
                    GameManager.EndGame(new GameplayFeedback(FeedbackLevel.Info, "You lost! You got exploded!"), GameResolution.Loss);
                }
                
                entity.Kill();
            }

            if (!map.Units.Any(entity => entity is IEnemy))
            {
                GameManager.EndGame(new GameplayFeedback(FeedbackLevel.Info, $"You won! The game lasted: {GameManager.Timer.Elapsed:g}"), GameResolution.Win);
                GameManager.Timer.Reset();
            }
    
        }
        
        public void HandleKeyPress(char keyChar, IBomber bomber)
        {
            var map = BoardService.GetActiveMap<IBomberMap, IBomberMapSource, IMapView2D>();

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
            var map = BoardService.GetActiveMap<IBomberMap, IBomberMapSource, IMapView2D>();
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
            if (GameManager.State == GameState.Paused)
            {
                GameManager.ResumeGame();
            }
            else
            {
                GameManager.PauseGame();
            }
        }
    }
}
