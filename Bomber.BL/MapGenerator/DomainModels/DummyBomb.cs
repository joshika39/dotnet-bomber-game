namespace Bomber.BL.MapGenerator.DomainModels
{
    public class DummyBomb
    {
        public int X { get; set;}
        public int Y { get; set;}
        
        public double RemainingTime { get; set; }
        
        public DummyBomb()
        { }
        
        public DummyBomb(int x, int y, double remainingTime)
        {
            X = x;
            Y = y;
            RemainingTime = remainingTime;
        }

        public override string ToString()
        {
            return $"Enemy ({X}, {Y})";
        }
    }
}