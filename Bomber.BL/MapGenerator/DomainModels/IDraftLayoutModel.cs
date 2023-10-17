using Infrastructure.Repositories;

namespace Bomber.BL.MapGenerator.DomainModels
{
    public interface IDraftLayoutModel : IEntity
    {
        string Name { get; set; }
        string Description { get; set; }
        int ColumnCount { get; set; }
        int RowCount { get; set; }
    }
}
