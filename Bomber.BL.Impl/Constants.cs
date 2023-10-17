using Bomber.BL.Map;

namespace Bomber.BL.Impl
{
    public class Constants
    {
        public static int TileTypeToInt(TileType tileType)
        {
            var types = Enum.GetValues(typeof(TileType)).Cast<TileType>().ToList();
            return types.ToList().IndexOf(tileType);
        }

        public static TileType IntToTileType(int tileType)
        {
            var types = Enum.GetValues(typeof(TileType)).Cast<TileType>().ToList();
            return types[tileType];
        }

        public static TileType GetNextTileType(TileType tileType)
        {
            var types = Enum.GetValues(typeof(TileType)).Cast<TileType>().ToList();
            var index = types.ToList().IndexOf(tileType);
            index++;
            if (index >= types.Count)
            {
                index = 0;
            }
            return  types[index];
        }

        public static void CreateFile(string path)
        {
            if (File.Exists(path)) return;
            
            File.Create(path).Close();
        }

        public static void CreateDirectory(string path)
        {
            var directory = Path.GetDirectoryName(path) ?? "";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
        
        public static void CreateFileAndDirectory(string path)
        {
            CreateDirectory(path);
            CreateFile(path);
        }
    }
}
