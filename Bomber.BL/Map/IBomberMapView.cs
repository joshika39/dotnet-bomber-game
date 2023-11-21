using Bomber.UI.Shared.Entities;
using GameFramework.Visuals;

namespace Bomber.BL.Map
{
    public interface IBomberMapView : IMapView2D
    {
        void PlantBomb(IBombView bombView);
        void DeleteBomb(IBombView bombView);
    }
}
