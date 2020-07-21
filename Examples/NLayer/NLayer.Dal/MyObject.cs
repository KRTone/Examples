namespace NLayer.Dal
{
    class MyObject
    {
        public MyObject(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        int x;
        public int X => x;

        int y;
        public int Y => y;

        public override string ToString()
        {
            return $"X: {x}, Y:{y}";
        }
    }
}
