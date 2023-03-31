namespace SokobanBruteforcer
{
    public interface ILevel
    {
        public static Dictionary<byte, (int, int)> _solutions;

        public byte[,] Grid { get; set; }
        public ILevel PreviousLevel { get; set; }
        public short StepsCount { get; set; }
        //public (int x, int y) HeroIndex { get; set; }
        (int x, int y) FindHeroIndex();
    }
}
