﻿using System.Collections.ObjectModel;
using Bomber.BL.Entities;
using Bomber.BL.Tiles;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Entities
{
    public sealed class PlayerModel : IBomber
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
            _view.UpdatePosition(Position);
        }
        public ICollection<IBomb> PlantedBombs { get; }

        public void PutBomb(IBombView bombView, IEnumerable<IBombWatcher> bombWatchers)
        {
            var bomb = new Bomb(bombView, Position, _configurationService2D, bombWatchers, 3, _cancellationToken);
            PlantedBombs.Add(bomb);
        }

        public void PutBomb(IBombView bombView, IBombWatcher? bombWatcher)
        {
            PutBomb(bombView, new List<IBombWatcher> { bombWatcher ?? this });
        }

        public PlayerModel(IPlayerView view, IPosition2D position, IConfigurationService2D configurationService2D, string name, string email, CancellationToken cancellationToken)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _configurationService2D = configurationService2D ?? throw new ArgumentNullException(nameof(configurationService2D));
            _cancellationToken = cancellationToken;
            Position = position ?? throw new ArgumentNullException(nameof(position));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Id = Guid.NewGuid();
            _view.EntityLoaded += OnViewLoad;
            PlantedBombs = new ObservableCollection<IBomb>();
        }

        private void OnViewLoad(object? sender, EventArgs e)
        {
            _view.UpdatePosition(Position);
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
            Dispose();
        }

        public void BombExploded(IBomb bomb)
        {
            PlantedBombs.Remove(bomb);
        }
    }
}
