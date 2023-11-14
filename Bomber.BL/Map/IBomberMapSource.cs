using Bomber.BL.MapGenerator.DomainModels;
using GameFramework.Map;

namespace Bomber.BL.Map
{
    public interface IBomberMapSource : IMapSource2D
    {
        string Name { get; set; }
        string Description { get; set; }
        IEnumerable<DummyEntity> Enemies { get; set; }
    }
}
