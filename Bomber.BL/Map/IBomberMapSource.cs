using Bomber.BL.Entities;
using GameFramework.Core;
using GameFramework.Map.MapObject;
using Bomber.BL.MapGenerator.DomainModels;
using GameFramework.Map;

namespace Bomber.BL.Map
{
    public interface IBomberMapSource : IMapSource2D
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
