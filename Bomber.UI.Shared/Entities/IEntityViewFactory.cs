namespace Bomber.UI.Shared.Entities
{
    public interface IEntityViewFactory
    {
        IEnemyView CreateEnemyView();
        IBombView CreateBombView();
        IPlayerView CreatePlayerView();
    }
}
