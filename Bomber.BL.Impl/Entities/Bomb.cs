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
        private readonly IEnumerable<IBombWatcher> _bombWatchers;
        private readonly IGameManager _gameManager;
        private readonly IBoardService _boardService;
        private readonly IEnumerable<IMapObject2D> _affectedObjects = new List<IMapObject2D>();
        private bool _disposed;
        private readonly CancellationToken _stoppingToken;
        private int _countDownPeriod = 2000;
        public IPosition2D Position { get; }
        public IScreenSpacePosition ScreenSpacePosition { get; }
        
        public bool IsObstacle => false;
        public int Radius { get; }
        public IBombView View { get; }
        private bool _isDetonated;


        public Bomb(IBombView view, IPosition2D position, IEnumerable<IBombWatcher> bombWatchers, int radius, IGameManager gameManager, ILifeCycleManager lifeCycleManager, IBoardService boardService)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
            _bombWatchers = bombWatchers ?? throw new ArgumentNullException(nameof(bombWatchers));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
            _boardService = boardService ?? throw new ArgumentNullException(nameof(boardService));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            lifeCycleManager = lifeCycleManager ?? throw new ArgumentNullException(nameof(lifeCycleManager));
            _stoppingToken = lifeCycleManager.Token;
            if (radius <= 0)
            {
                throw new InvalidOperationException("Radius cannot be zero or negative");
            }

            Radius = radius;

            if (_gameManager.State != GameState.InProgress)
            {
                Dispose();
            }

            var map = _boardService.GetActiveMap<IBomberMap, IBomberMapSource, IBomberMapView>();
            _affectedObjects = map!.MapPortion(position, radius);
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
                _countDownPeriod -= 300;

                if (_countDownPeriod <= 0)
                {
                    Explode();
                    break;
                }

                await _gameManager.Timer.WaitAsync(_countDownPeriod, this);
            }

            Dispose();
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
                    bombMapObject.IndicateBomb(_countDownPeriod / 1000d);
                }
            }
        }
        
        public TimeSpan ElapsedTime { get; set; }
        
        public void OnLoaded()
        {
            View.UpdatePosition(Position);
        }
        public void OnHovered()
        {
            throw new NotImplementedException();
        }
        public void OnHoverLost()
        {
            throw new NotImplementedException();
        }
        public bool IsHovered
        {
            get;
        }
    }
}
