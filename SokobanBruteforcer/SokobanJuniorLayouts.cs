namespace SokobanBruteforcer
{
    public class SokobanJuniorLayouts
    {

        public static byte[,] SokobanJunior1 = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0 }, //7, 7
            { 0, 1, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 3, 1, 1, 0 },
            { 0, 1, 3, 2, 3, 1, 0 },
            { 0, 1, 1, 3, 1, 1, 0 },
            { 0, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };

        public static Dictionary<(int, int), byte> SokobanJunior1Solutions = new Dictionary<(int, int), byte>()
        {
            { (1, 1), 3 },
            { (1, 5), 3 },
            { (5, 1), 3 },
            { (5, 5), 3 },
        };


        public static byte[,] SokobanJunior4 = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0 }, //7, 7
            { 0, 1, 1, 1, 1, 1, 0 },
            { 0, 1, 3, 2, 3, 1, 0 },
            { 0, 1, 0, 0, 0, 1, 0 },
            { 0, 1, 3, 1, 3, 1, 0 },
            { 0, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };

        public static Dictionary<(int, int), byte> SokobanJunior4Solutions = new Dictionary<(int, int), byte>()
        {
            { (1, 1), 3 },
            { (1, 5), 3 },
            { (5, 1), 3 },
            { (5, 5), 3 },
        };


        public static byte[,] SokobanJunior15 = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //11, 11
            { 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 1, 3, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 3, 1, 3, 1, 3, 1, 1, 0 },
            { 0, 1, 1, 1, 3, 3, 3, 1, 1, 1, 0 },
            { 0, 0, 1, 1, 3, 2, 3, 1, 1, 0, 0 },
            { 0, 1, 1, 1, 3, 3, 3, 1, 1, 1, 0 },
            { 0, 1, 1, 3, 1, 3, 1, 3, 1, 1, 0 },
            { 0, 1, 1, 1, 1, 3, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };

        public static Dictionary<(int, int), byte> SokobanJunior15Solutions = new Dictionary<(int, int), byte>()
        {
            { (1, 1), 3 },
            { (1, 2), 3 },
            { (2, 1), 3 },
            { (2, 2), 3 },

            { (1, 8), 3 },
            { (1, 9), 3 },
            { (2, 8), 3 },
            { (2, 9), 3 },

            { (8, 1), 3 },
            { (8, 2), 3 },
            { (9, 1), 3 },
            { (9, 2), 3 },

            { (8, 8), 3 },
            { (8, 9), 3 },
            { (9, 8), 3 },
            { (9, 9), 3 },
        };

        public static Predicate<(int x, int y)> Level15InvalidationImprovement =>
           (boxIndices) => (boxIndices.x == 4 && boxIndices.y == 1) || (boxIndices.x == 6 && boxIndices.y == 1) || //left side stuck
           (boxIndices.x == 4 && boxIndices.y == 9) || (boxIndices.x == 6 && boxIndices.y == 9) || //right side stuck
           (boxIndices.x == 1 && boxIndices.y == 4) || (boxIndices.x == 1 && boxIndices.y == 6) || // top side stuck
           (boxIndices.x == 9 && boxIndices.y == 4) || (boxIndices.x == 9 && boxIndices.y == 6); // bottom side stuck


        //0 = wall
        //1 = empty
        //2 = hero
        //3 = any box
    }
}
