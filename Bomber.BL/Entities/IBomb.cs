using GameFramework.Core;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Entities
{
    public interface IBomb : IMapObject2D, IDisposable
    {
        int Radius { get; }
        Task Detonate();
    }
}
