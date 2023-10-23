using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.BL.Tiles;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Motion;
using GameFramework.Entities;
using GameFramework.Map.MapObject;
using GameFramework.Time;
using GameFramework.Time.Listeners;

namespace Bomber.BL.Impl.Entities
{
    public sealed class Enemy : IEnemy, ITickListener
    {
        private readonly IEnemyView _view;
        private readonly IStopwatch _stopwatch;
        private readonly CancellationToken _stoppingToken;
        private readonly IBomberMap _map;
        private Move2D _direction;
        private bool _disposed;

        public IPosition2D Position { get; private set; }
        public bool IsObstacle => false;

        public Enemy(IEnemyView view, IConfigurationService2D service, IPosition2D position, IStopwatch stopwatch)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _stopwatch = stopwatch ?? throw new ArgumentNullException(nameof(stopwatch));
            service = service ?? throw new ArgumentNullException(nameof(service));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            _stoppingToken = service.CancellationTokenSource.Token;
            _map = service.GetActiveMap<IBomberMap>()!;
            _direction = GetRandomMove();
            _view.EntityLoaded += OnViewLoad;
        }
        
        private void OnViewLoad(object? sender, EventArgs e)
        {
            _view.UpdatePosition(Position);
        }

        public async Task ExecuteAsync()
        {
            while (!_stoppingToken.IsCancellationRequested && !_disposed)
            {
                var newPeriodInSeconds = System.Security.Cryptography.RandomNumberGenerator.GetInt32(1, 3);
                
                await _stopwatch.WaitAsync(newPeriodInSeconds * 1000, this);
            }
        }
        
        public void SteppedOn(IUnit2D unit2D)
        {
            throw new NotImplementedException();
        }
        
        public void Step(IMapObject2D mapObject)
        {
            if (mapObject is IDeadlyTile || mapObject.IsObstacle)
            {
                _direction = GetRandomMove();
            }
            Position = mapObject.Position;
            _view.UpdatePosition(Position);
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
                _view.Dispose();
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
            var mapObject = _map.SimulateMove(Position, _direction);
            while (mapObject is null || mapObject.IsObstacle || mapObject is IDeadlyTile || _map.HasEnemy(mapObject.Position))
            {
                _direction = GetRandomMove();
                mapObject = _map.SimulateMove(Position, _direction);
            }
                    
            Step(mapObject);
        }
        
        public TimeSpan ElapsedTime { get; set; }
    }
}
