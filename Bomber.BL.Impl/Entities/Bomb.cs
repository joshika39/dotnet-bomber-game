using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using Bomber.UI.Shared.Views;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Entities
{
    public sealed class Bomb : IBomb
    {
        private readonly IEnumerable<IBombWatcher> _bombWatchers;
        private readonly CancellationToken _stoppingToken;
        private PeriodicTimer? _timer;
        private readonly IEnumerable<IMapObject2D> _affectedObjects;
        private bool _disposed;
        public IPosition2D Position { get; }
        public bool IsObstacle => false;
        public int Radius { get; }
        public IBombView View { get; }


        public Bomb(IBombView view, IPosition2D position, IConfigurationService2D configurationService, IEnumerable<IBombWatcher> bombWatchers, int radius,  CancellationToken stoppingToken)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
            _bombWatchers = bombWatchers ?? throw new ArgumentNullException(nameof(bombWatchers));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));

            if (radius <= 0)
            {
                throw new InvalidOperationException("Radius cannot be zero or negative");
            }
            
            Radius = radius;
            _stoppingToken = stoppingToken;

            if (!configurationService.GameIsRunning)
            {
                Dispose();
            }

            var map = configurationService.GetActiveMap<IBomberMap>();
            _affectedObjects = map!.MapPortion(position, radius);
            View.EntityLoaded += OnViewLoaded;
        }

        private void OnViewLoaded(object? sender, EventArgs e)
        {
            View.UpdatePosition(Position);
        }

        public async Task Detonate()
        {
            var countDownPeriod = 2d;
            while (!_stoppingToken.IsCancellationRequested)
            {

                countDownPeriod -= 0.3;


                if (countDownPeriod <= 0)
                {
                    Explode();
                    break;
                }

                var time = TimeSpan.FromSeconds(countDownPeriod);

                _timer = new PeriodicTimer(time);

                if (await _timer.WaitForNextTickAsync(_stoppingToken))
                {
                    foreach (var affectedObject in _affectedObjects)
                    {
                        if (affectedObject is IBomberMapTileView bombMapObject)
                        {
                            bombMapObject.IndicateBomb(countDownPeriod / 10);
                        }
                    }
                }

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
        { }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _timer?.Dispose();
                View.Dispose();
            }

            _disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
        }
    }
}
