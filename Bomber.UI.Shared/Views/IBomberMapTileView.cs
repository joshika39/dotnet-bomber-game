using GameFramework.Map.MapObject;

namespace Bomber.UI.Shared.Views
{
    public interface IBomberMapTileView : IMapObject2D
    {
        void IndicateBomb(double waitTime);
    }
}
