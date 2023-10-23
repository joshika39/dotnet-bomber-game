using Bomber.BL.MapGenerator.DomainModels;
using Implementation.Repositories;

namespace Bomber.BL.Impl.MapGenerator.DomainModels
{
    public class DraftLayoutModel : AEntity, IDraftLayoutModel
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
        public int PlayerXPos { get; set; }
        public int PlayerYPos { get; set; }
    }
}
