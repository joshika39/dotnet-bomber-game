using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using Bomber.UI.Shared.Views;
using GameFramework.Board;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Entities;
using GameFramework.Manager;
using GameFramework.Map.MapObject;
using GameFramework.Visuals;
using Infrastructure.Application;
using Infrastructure.Time.Listeners;

namespace Bomber.BL.Impl.Entities
{
    public sealed class Bomb : IBomb, ITickListener, IViewLoadedSubscriber
    {
        private readonly ICollection<IBombWatcher> _bombWatchers = new List<IBombWatcher>();
        private readonly IGameManager _gameManager;
        private readonly IEnumerable<IMapObject2D> _affectedObjects = new List<IMapObject2D>();
        private bool _disposed;
        private readonly CancellationToken _stoppingToken;

        public Guid Id { get; } = Guid.NewGuid();
        public IPosition2D Position { get; }
        public IScreenSpacePosition ScreenSpacePosition { get; }
        
        public bool IsObstacle => false;
        public int Radius { get; }
        public IDynamicMapObjectView View { get; }
        public int RemainingTime { get; private set; }
        private bool _isDetonated;


        public Bomb(IBombView view, IPosition2D position, int radius, IGameManager gameManager, ILifeCycleManager lifeCycleManager, IBoardService boardService, int timeToExplosion = 2000)
        {
            RemainingTime = timeToExplosion;
            View = view ?? throw new ArgumentNullException(nameof(view));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
            var boardService1 = boardService ?? throw new ArgumentNullException(nameof(boardService));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            lifeCycleManager = lifeCycleManager ?? throw new ArgumentNullException(nameof(lifeCycleManager));
            _stoppingToken = lifeCycleManager.Token;
            if (radius <= 0)
            {
                throw new InvalidOperationException("Radius cannot be zero or negative");
            }

            ScreenSpacePosition = view.PositionOnScreen;

            Radius = radius;

            if (_gameManager.State != GameState.InProgress)
            {
                Dispose();
            }

            var map = boardService1.GetActiveMap<IBomberMap, IBomberMapSource, IBomberMapView>();
            if (map is not null)
            {
                _affectedObjects = map.MapPortion(position, radius);
            }
            View.Attach(this);
        }

        public async Task Detonate()
        {
            if(_disposed || _isDetonated)
            {
                return;
            }

            _isDetonated = true;
            while (!_stoppingToken.IsCancellationRequested)
            {
                RemainingTime -= 300;

                if (RemainingTime <= 0)
                {
                    Dispose();
                    Explode();
                    break;
                }

                await _gameManager.Timer.WaitAsync(RemainingTime, this);
            }

        }
        
        public void Attach(IBombWatcher bombWatcher)
        {
            if (!_bombWatchers.Contains(bombWatcher))
            {
                _bombWatchers.Add(bombWatcher);
            }
        }

        private void Explode()
        {
            foreach (var bombWatcher in _bombWatchers)
            {
                bombWatcher.BombExploded(this);
            }
        }

        public void SteppedOn(IUnit2D unit2D)
        {
            
        }
        
        public void Step(IMapObject2D mapObject)
        {
            throw new NotSupportedException();
        }
        
        public void Kill()
        {
            throw new NotSupportedException();
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
            }

            _disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
        }
        
        public void RaiseTick(int round)
        {
            foreach (var affectedObject in _affectedObjects)
            {
                if (affectedObject is IBomberMapTileView bombMapObject)
                {
                    bombMapObject.IndicateBomb(RemainingTime / 1000d);
                }
            }
        }
        
        public TimeSpan ElapsedTime { get; set; }
        
        public void OnLoaded()
        {
            View.UpdatePosition(Position);
        }
    }
}
