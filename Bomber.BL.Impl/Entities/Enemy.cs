using Bomber.BL.Entities;
using Bomber.BL.Feedback;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using GameFramework.Board;
using GameFramework.Core;
using GameFramework.Core.Motion;
using GameFramework.Core.Position;
using GameFramework.Entities;
using GameFramework.GameFeedback;
using GameFramework.Manager;
using GameFramework.Map.MapObject;
using GameFramework.Tiles;
using GameFramework.Visuals;
using Infrastructure.Application;
using Infrastructure.Time.Listeners;

namespace Bomber.BL.Impl.Entities
{
    public sealed class Enemy : IEnemy, ITickListener, IViewLoadedSubscriber
    {
        public Guid Id { get; } = Guid.NewGuid();
        public IDynamicMapObjectView View { get; }

        private readonly CancellationToken _stoppingToken;
        private Move2D _direction;
        private bool _disposed;
        private readonly IBoardService _service;
        private readonly IGameManager _gameManager;

        public IPosition2D Position { get; private set; }
        public IScreenSpacePosition ScreenSpacePosition { get; }
        public bool IsObstacle => false;

        public Enemy(IEnemyView view, IBoardService service, IPosition2D position, IGameManager gameManager, ILifeCycleManager lifeCycleManager)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            _stoppingToken = lifeCycleManager.Token;
            _direction = GetRandomMove();
            View.Attach(this);
            ScreenSpacePosition = view.PositionOnScreen;
        }

        public async Task ExecuteAsync()
        {
            while (!_stoppingToken.IsCancellationRequested && !_disposed)
            {
                var newPeriodInSeconds = System.Security.Cryptography.RandomNumberGenerator.GetInt32(1, 3);
                
                await _gameManager.Timer.WaitAsync(newPeriodInSeconds * 1000, this);
            }
        }
        
        public void SteppedOn(IUnit2D unit2D)
        {
            if (unit2D is IBomber)
            {
                _gameManager.EndGame(new GameplayFeedback(FeedbackLevel.Info, "You died!"), GameResolution.Loss);
            }
        }
        
        public void Step(IMapObject2D mapObject)
        {
            if (mapObject is IDeadlyTile || mapObject.IsObstacle)
            {
                _direction = GetRandomMove();
            }
            Position = mapObject.Position;
            View.UpdatePosition(Position);
        }

        private static Move2D GetRandomMove()
        {
            return System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, 4) switch
            {
                0 => Move2D.Left,
                1 => Move2D.Right,
                2 => Move2D.Forward,
                3 => Move2D.Backward,
                _ => throw new InvalidOperationException("Unsupported move!")
            };
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
        
        public void Kill()
        {
            Dispose();
        }
        
        public void RaiseTick(int round)
        {
            var map = _service.GetActiveMap<IBomberMap>();
            
            if(map is null)
            {
                return;
            }
            
            var mapObject = map.SimulateMove(Position, _direction);
            while (mapObject is null || mapObject.IsObstacle || mapObject is IDeadlyTile || map.HasEnemy(mapObject.Position))
            {
                _direction = GetRandomMove();
                mapObject = map.SimulateMove(Position, _direction);
            }
            map.MoveUnit(this, _direction);
        }
        
        public TimeSpan ElapsedTime { get; set; }
        
        public void OnLoaded()
        {
            View.UpdatePosition(Position);
        }
    }
}
