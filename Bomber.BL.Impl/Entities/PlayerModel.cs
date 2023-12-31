﻿using System.Collections.ObjectModel;
using Bomber.BL.Entities;
using Bomber.BL.Feedback;
using Bomber.UI.Shared.Entities;
using GameFramework.Board;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Entities;
using GameFramework.GameFeedback;
using GameFramework.Manager;
using GameFramework.Map.MapObject;
using GameFramework.Tiles;
using GameFramework.Visuals;
using Infrastructure.Application;

namespace Bomber.BL.Impl.Entities
{
    public sealed class PlayerModel : IBomber, IViewLoadedSubscriber
    {
        private readonly IGameManager _gameManager;
        private readonly ILifeCycleManager _lifeCycleManager;
        private readonly IBoardService _boardService;
        private bool _isAlive = true;
        private bool _disposed;
        public IPosition2D Position { get; private set; }
        public IScreenSpacePosition ScreenSpacePosition { get; }
        public bool IsObstacle => false;
        public Guid Id { get; }
        public IDynamicMapObjectView View { get; }
        public string Name { get; }
        public string Email { get; }

        public void SteppedOn(IUnit2D unit2D)
        {
            if (unit2D is IEnemy)
            {
                Kill();
            }
        }

        public void Step(IMapObject2D mapObject)
        {
            if (_gameManager.State != GameState.InProgress || !_isAlive)
            {
                return;
            }

            if (mapObject is IDeadlyTile)
            {
                Kill();
            }

            Position = mapObject.Position;
            View.UpdatePosition(Position);
        }
        
        public ICollection<IBomb> PlantedBombs { get; }

        public IBomb PutBomb(IBombView bombView, IBombWatcher bombWatcher)
        {
            throw new NotImplementedException();
        }
        public void DetonateBombAt(int bombIndex)
        {
            if (!PlantedBombs.Any())
            {
                return;
            }
            
            if(bombIndex < 0 || bombIndex >= PlantedBombs.Count)
            {
                return;
            }

            var bomb = PlantedBombs.ElementAt(bombIndex);
            bomb.Detonate();
        }
        
        public int Score { get; set; }

        public IBomb PutBomb(IBombView bombView)
        {
            var bomb = new Bomb(bombView, Position, 3, _gameManager, _lifeCycleManager, _boardService);
            bomb.Attach(this);
            PlantedBombs.Add(bomb);
            return bomb;
        }

        public PlayerModel(IPlayerView view, IPosition2D position, string name, string email, IGameManager gameManager, ILifeCycleManager lifeCycleManager, IBoardService boardService)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
            _lifeCycleManager = lifeCycleManager ?? throw new ArgumentNullException(nameof(lifeCycleManager));
            _boardService = boardService ?? throw new ArgumentNullException(nameof(boardService));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Id = Guid.NewGuid();
            PlantedBombs = new ObservableCollection<IBomb>();
            View.Attach(this);
            ScreenSpacePosition = view.PositionOnScreen;
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                View.Dispose();
                while (PlantedBombs.Count > 0)
                {
                    var bomb = PlantedBombs.ElementAt(PlantedBombs.Count - 1);
                    PlantedBombs.Remove(bomb);
                    bomb.Dispose();
                }
            }

            _disposed = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
        }

        public void Kill()
        {
            _isAlive = false;
            _gameManager.EndGame(new GameplayFeedback(FeedbackLevel.Info, "You lost because you're DEAD!"), GameResolution.Loss);
            Dispose();
        }

        public void BombExploded(IBomb bomb)
        {
            PlantedBombs.Remove(bomb);
        }
        
        public void OnLoaded()
        {
            View.UpdatePosition(Position);
        }
    }
}
