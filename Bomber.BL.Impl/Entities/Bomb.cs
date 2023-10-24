using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using Bomber.UI.Shared.Views;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;
using GameFramework.Time;
using GameFramework.Time.Listeners;

namespace Bomber.BL.Impl.Entities
{
    public sealed class Bomb : IBomb, ITickListener, IEntityViewSubscriber
    {
        private readonly IEnumerable<IBombWatcher> _bombWatchers;
        private readonly IStopwatch _stopwatch;
        private readonly IEnumerable<IMapObject2D> _affectedObjects;
        private bool _disposed;
        private readonly CancellationToken _stoppingToken;
        private int _countDownPeriod = 2000;
        public IPosition2D Position { get; }
        public bool IsObstacle => false;
        public int Radius { get; }
        public IBombView View { get; }
        private bool _isDetonated;


        public Bomb(IBombView view, IPosition2D position, IConfigurationService2D configurationService, IEnumerable<IBombWatcher> bombWatchers, int radius, IStopwatch stopwatch)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
            _bombWatchers = bombWatchers ?? throw new ArgumentNullException(nameof(bombWatchers));
            _stopwatch = stopwatch ?? throw new ArgumentNullException(nameof(stopwatch));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _stoppingToken = configurationService.CancellationTokenSource.Token;
            if (radius <= 0)
            {
                throw new InvalidOperationException("Radius cannot be zero or negative");
            }

            Radius = radius;

            if (!configurationService.GameIsRunning)
            {
                Dispose();
            }

            var map = configurationService.GetActiveMap<IBomberMap>();
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

                await _stopwatch.WaitAsync(_countDownPeriod, this);

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
        
        public void OnViewLoaded()
        {
            View.UpdatePosition(Position);
        }
    }
}
