using Bomber.BL.Map;
using GameFramework.Entities;
using GameFramework.Impl.Map.Source;

namespace Bomber.BL.Impl.Map
{
    public class BomberMapSource : JsonMapSource2D, IBomberMapSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public BomberMapSource(IServiceProvider provider, string filePath, int[,] data, ICollection<IUnit2D> units, int col, int row, string name, string description) : base(provider, filePath, data, units, col, row)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        // TODO: When _query is protected add the restoring part
        public BomberMapSource(IServiceProvider provider, string filePath) : base(provider, filePath)
        {
            
        }
    }
}
