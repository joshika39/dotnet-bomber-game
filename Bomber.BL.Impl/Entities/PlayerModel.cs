using System.Collections.ObjectModel;
using Bomber.BL.Entities;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Entities
{
    public class PlayerModel : IBomber, IBombWatcher
    {
        private readonly IPlayerView _view;
        private readonly IConfigurationService2D _configurationService2D;
        private readonly CancellationToken _cancellationToken;
        private bool _isAlive = true;
        private bool _disposed;
        public IPosition2D Position { get; private set; }
        public bool IsObstacle => false;
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }

        public void SteppedOn(IUnit2D unit2D)
        {
            throw new NotImplementedException();
        }

        public void Step(IMapObject2D mapObject)
        {
            if (!_configurationService2D.GameIsRunning)
            {
                if (!_isAlive)
                {
                    return;
                }

                _isAlive = false;
            }
            
            Position = mapObject.Position;
            _view.UpdatePosition(Position);
        }
        public ICollection<IBomb> PlantedBombs { get; }
        
        public async void PutBomb(IBombView bombView, IBombWatcher bombWatcher)
        {
            var bombWatchers = new Collection<IBombWatcher>
            {
                bombWatcher,
                this
            };
            var bomb = new Bomb(bombView, Position, _configurationService2D, bombWatchers, 3, _cancellationToken);
            PlantedBombs.Add(bomb);
            await bomb.Detonate();
        }
        
        public PlayerModel(IPlayerView view, IPosition2D position, IConfigurationService2D configurationService2D, string name, string email, CancellationToken cancellationToken)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _configurationService2D = configurationService2D ?? throw new ArgumentNullException(nameof(configurationService2D));
            _cancellationToken = cancellationToken;
            Position = position;
            Name = name;
            Email = email;
            Id = Guid.NewGuid();
            _view.Load += OnViewLoad;
            PlantedBombs = new ObservableCollection<IBomb>();
        }
        
        private void OnViewLoad(object? sender, EventArgs e)
        {
            _view.UpdatePosition(Position);
        }
        
        protected virtual void Dispose(bool disposing)
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

        public void BombExploded(IBomb bomb)
        {
            PlantedBombs.Remove(bomb);
        }
    }
}
