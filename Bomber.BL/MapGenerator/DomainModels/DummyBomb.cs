namespace Bomber.BL.MapGenerator.DomainModels
{
    public class DummyBomb
    {
        public int X { get; set;}
        public int Y { get; set;}
        public int RemainingTime { get; set; }
        public int Radius { get; set; }
        
        public DummyBomb()
        { }
        
        public DummyBomb(int x, int y, int radius, int remainingTime)
        {
            X = x;
            Y = y;
            Radius = radius;
            RemainingTime = remainingTime;
        }

        public override string ToString()
        {
            return $"Enemy ({X}, {Y})";
        }
    }
}
