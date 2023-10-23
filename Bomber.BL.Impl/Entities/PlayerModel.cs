using System.Collections.ObjectModel;
using Bomber.BL.Entities;
using Bomber.BL.Feedback;
using Bomber.BL.Tiles;
using Bomber.UI.Shared.Entities;
using Bomber.UI.Shared.Views;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.GameFeedback;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Entities
{
    public sealed class PlayerModel : IBomber
    {
        public IBomberMapEntityView View { get; }
        private readonly IConfigurationService2D _configurationService2D;
        private readonly IGameManager _gameManager;
        private bool _isAlive = true;
        private bool _disposed;
        public IPosition2D Position { get; private set; }
        public bool IsObstacle => false;
        public Guid Id { get; }
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
            if (!_configurationService2D.GameIsRunning || !_isAlive)
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

        public void PutBomb(IBombView bombView, IBombWatcher? bombWatcher)
        {
            var bombWatchers = new List<IBombWatcher>
            {
                bombWatcher ?? this
            };
            
            if (!bombWatchers.Contains(this))
            {
                bombWatchers.Add(this);
            }
            
            var bomb = new Bomb(bombView, Position, _configurationService2D, bombWatchers, 3, _gameManager.Timer);
            PlantedBombs.Add(bomb);
        }

        public PlayerModel(IPlayerView view, IPosition2D position, IConfigurationService2D configurationService2D, string name, string email, IGameManager gameManager)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
            _configurationService2D = configurationService2D ?? throw new ArgumentNullException(nameof(configurationService2D));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Id = Guid.NewGuid();
            View.EntityLoaded += OnViewLoad;
            PlantedBombs = new ObservableCollection<IBomb>();
        }

        private void OnViewLoad(object? sender, EventArgs e)
        {
            View.UpdatePosition(Position);
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
                _isAlive = false;
                _configurationService2D.GameIsRunning = false;
            }

            _disposed = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
        }

        public void Kill()
        {
            _gameManager.GameFinished(new GameplayFeedback(FeedbackLevel.Info, "You lost because you're DEAD!"), GameResolution.Loss);
            Dispose();
        }

        public void BombExploded(IBomb bomb)
        {
            PlantedBombs.Remove(bomb);
        }
    }
}
