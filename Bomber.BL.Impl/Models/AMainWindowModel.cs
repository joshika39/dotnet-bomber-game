using System.Diagnostics;
using Bomber.BL.Entities;
using Bomber.BL.Feedback;
using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using GameFramework.Board;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Core.Motion;
using GameFramework.GameFeedback;
using GameFramework.Manager;
using GameFramework.Manager.State;
using GameFramework.Visuals;
using Infrastructure.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.BL.Impl.Models
{
    public class AMainWindowModel : ObservableObject, IMainWindowModel, IGameStateChangedListener, IBombWatcher, IViewDisposedSubscriber
    {
        protected readonly IServiceProvider Provider;
        protected readonly IPositionFactory PositionFactory;
        protected readonly IConfigurationService2D ConfigurationService;
        protected readonly IGameManager GameManager;
        protected readonly IBoardService BoardService;
        protected readonly ILifeCycleManager LifeCycleManager;
        protected readonly IEntityViewFactory EntityViewFactory;
        protected readonly IEntityFactory EntityFactory;
        private BomberMap? _map;
        private string? _previousFile;

        protected AMainWindowModel(IServiceProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            ConfigurationService = Provider.GetRequiredService<IConfigurationService2D>();
            GameManager = Provider.GetRequiredService<IGameManager>();
            BoardService = Provider.GetRequiredService<IBoardService>();
            PositionFactory = Provider.GetRequiredService<IPositionFactory>();
            EntityFactory = Provider.GetRequiredService<IEntityFactory>();
            EntityViewFactory = Provider.GetRequiredService<IEntityViewFactory>();
            LifeCycleManager = Provider.GetRequiredService<ILifeCycleManager>();
            GameManager.AttachListener(this);
        }

        public IBomberMap OpenMap(string mapFileName, IBomberMapView mapView2D)
        {
            _previousFile = mapFileName;
            GameManager.EndGame(new GameplayFeedback(FeedbackLevel.Info, "Game ended"), GameResolution.Nothing);

            var source = new BomberMapSource(Provider, mapFileName);
            _map = new BomberMap(source, mapView2D, PositionFactory, ConfigurationService, Provider.GetRequiredService<IEntityFactory>(), EntityViewFactory);

            if (GameManager.State == GameState.InProgress)
            {
                GameManager.ResetGame();
            }

            var view = EntityViewFactory.CreatePlayerView();
            var player = EntityFactory.CreatePlayer(view, _map.MapSource.PlayerPosition, "TestPlayer", "test@email.com");
            view.ViewLoaded();
            _map.Units.Add(player);

            GameManager.StartGame(new GameplayFeedback(FeedbackLevel.Info, "Game started!"));

            BoardService.SetActiveMap(_map);

            foreach (var unit in _map.Units)
            {
                unit.View.ViewLoaded();
                if (unit is IEnemy enemy)
                {
                    Task.Run(async () =>
                    {
                        await enemy.ExecuteAsync();
                    });
                }
            }

            return _map;
        }

        public virtual void BombExploded(IBomb bomb)
        {
            var map = BoardService.GetActiveMap<IBomberMap>();
            var unit = map?.Units.FirstOrDefault(b => b is IBomber);
            if (map is null || unit is not IBomber bomber)
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
                if (entity is IBomber)
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

        public void HandleKeyPress(char keyChar)
        {
            var map = BoardService.GetActiveMap<IBomberMap>();
            var unit = map?.Units.FirstOrDefault(b => b is IBomber);
            if (map is null || unit is not IBomber bomber)
            {
                return;
            }

            switch (keyChar)
            {
                case 'd':
                    map.MoveUnit(bomber, Move2D.Right);
                    break;
                case 'a':
                    map.MoveUnit(bomber, Move2D.Left);
                    break;
                case 'w':
                    map.MoveUnit(bomber, Move2D.Forward);
                    break;
                case 's':
                    map.MoveUnit(bomber, Move2D.Backward);
                    break;
                case 'p':
                    PauseGame();
                    break;
                case 'r':
                    GameManager.ResetGame();
                    break;
                case 'b':
                    var bombView = EntityViewFactory.CreateBombView();
                    bomber.PutBomb(bombView, this);
                    bombView.Attach(this);
                    map.View.PlantBomb(bombView);
                    bombView.ViewLoaded();
                    break;
            }

            if (int.TryParse(keyChar.ToString(), out var bombIndex))
            {
                bomber.DetonateBombAt(bombIndex - 1);
            }

        }

        public void OnGameFinished(IGameplayFeedback feedback, GameResolution resolution)
        {
            var map = BoardService.GetActiveMap<IBomberMap>();
            if (map is null)
            {
                return;
            }

            foreach (var unit in map.Units)
            {
                unit.Dispose();
            }

            map.Units.Clear();
        }

        public void OnGamePaused()
        {
            Debug.WriteLine("Game paused");
        }

        public void OnGameResumed()
        {
            Debug.WriteLine("Game resumed");
        }

        public void OnGameStarted(IGameplayFeedback feedback)
        {
            Debug.WriteLine("Game started");
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

        public void OnViewDisposed(IDynamicMapObjectView view)
        {
            var map = BoardService.GetActiveMap<IBomberMap>();
            if (map is null || view is not IBombView bombView)
            {
                return;
            }

            map.View.DeleteBomb(bombView);
        }

        public virtual void OnGameReset()
        {
            if (_map is null || _previousFile is null)
            {
                return;
            }

            _map.Units.Clear();
            _map.MapObjects.Clear();

            var tmpMap = OpenMap(_previousFile, _map.View);

            if (tmpMap is BomberMap map)
            {
                _map = map;
            }
        }
    }
}
