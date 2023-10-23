namespace Bomber.BL.MapGenerator.DomainModels
{
    public class DummyEntity
    {
        public int X { get; set;}
        public int Y { get; set;}
        
        public DummyEntity()
        { }
        
        public DummyEntity(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"Enemy ({X}, {Y})";
        }
    }
}
