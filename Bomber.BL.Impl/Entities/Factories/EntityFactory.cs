using Bomber.BL.Entities;
using Bomber.UI.Shared.Entities;
using GameFramework.Board;
using GameFramework.Core;
using GameFramework.Core.Position;
using Infrastructure.Application;

namespace Bomber.BL.Impl.Entities.Factories
{
    internal class EntityFactory : IEntityFactory
    {
        private readonly IGameManager _gameManager;
        private readonly ILifeCycleManager _lifeCycleManager;
        private readonly IBoardService _boardService;
        public EntityFactory(IGameManager gameManager, ILifeCycleManager lifeCycleManager, IBoardService boardService)
        {
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
            _lifeCycleManager = lifeCycleManager ?? throw new ArgumentNullException(nameof(lifeCycleManager));
            _boardService = boardService ?? throw new ArgumentNullException(nameof(boardService));
        }

        public IBomb CreateBomb(IBombView view, IPosition2D position2D, IEnumerable<IBombWatcher> bombWatchers, int radius)
        {
            return new Bomb(view, position2D, bombWatchers, radius, _gameManager, _lifeCycleManager, _boardService);
        }
        
        public IBomber CreatePlayer(IPlayerView view, IPosition2D position, string name, string email)
        {
            return new PlayerModel(view, position, name, email, _gameManager, _lifeCycleManager, _boardService);
        }
        
        public IEnemy CreateEnemy(IEnemyView view, IPosition2D position2D)
        {
            return new Enemy(view, _boardService, position2D, _gameManager, _lifeCycleManager);
        }
    }
}
