namespace SokobanBruteforcer
{
    public interface ILevel
    {
        public byte[,] Grid { get; set; }
        public static Dictionary<byte, (int, int)> _solutions;
        public ILevel PreviousLevel { get; set; }
        public short StepsCount { get; set; }
        public (int x, int y) HeroIndex { get; set; }
    }
}
