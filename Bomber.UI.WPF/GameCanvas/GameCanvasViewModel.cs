using System;
using System.Collections.ObjectModel;
using Bomber.BL.Entities;
using Bomber.BL.Feedback;
using Bomber.BL.Impl.Entities;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using Bomber.UI.Shared.Views;
using Bomber.UI.WPF.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.GameFeedback;
using GameFramework.Map.MapObject;

namespace Bomber.UI.WPF.GameCanvas
{
    internal class GameCanvasViewModel : ObservableObject, IGameCanvasViewModel
    {
        private readonly IConfigurationService2D _configurationService;
        private readonly IPositionFactory _positionFactory;
        private readonly IGameManager _gameManager;
        private ObservableCollection<IBomberMapEntityView> _entityViews = new();
        private ObservableCollection<IMapObject2D> _mapObjects = new();

        public ObservableCollection<IBomberMapEntityView> EntityViews
        {
            get => _entityViews;
            private set => SetProperty(ref _entityViews, value);
        }
        
        public ObservableCollection<IMapObject2D> MapObjects
        {
            get => _mapObjects;
            private set => SetProperty(ref _mapObjects, value);
        }


        public GameCanvasViewModel(IConfigurationService2D configurationService, IPositionFactory positionFactory, IGameManager gameManager)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _positionFactory = positionFactory ?? throw new ArgumentNullException(nameof(positionFactory));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }
        
        public void StartGame(IBomberMap map)
        {
            var view = new PlayerControl(_configurationService);
            var player = new PlayerModel(view, _positionFactory.CreatePosition(3, 1), _configurationService, "TestPlayer", "test@email.com", _gameManager);
            view.EntityViewLoaded();
            map.Entities.Add(player);
            
            var mapObjects = new ObservableCollection<IMapObject2D>();
            foreach (var mapMapObject in map.MapObjects)
            {
                mapObjects.Add(mapMapObject);
            }
            MapObjects = mapObjects;

            var list = new ObservableCollection<IBomberMapEntityView>();
            
            foreach (var entity in map.Entities)
            {
                if (entity is not IBomberEntity bomberEntity)
                {
                    continue;
                }
                
                list.Add(bomberEntity.View);
            }
            EntityViews = list;
            
            _gameManager.GameStarted(new GameplayFeedback(FeedbackLevel.Info, "Game started!"));
        }

        public void BombExploded(IBomb bomb)
        {
            throw new System.NotImplementedException();
        }
    }
}
