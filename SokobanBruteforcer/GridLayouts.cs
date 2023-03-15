namespace SokobanBruteforcer
{
    public class GridLayouts
    {
        public static byte[,] Level58 = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //15, 8
            { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 4, 1, 1, 0, 0, 0, 1, 1, 5, 1, 1, 0 },
            { 0, 0, 3, 0, 2, 1, 1, 6, 1, 1, 1, 0, 7, 0, 0 },
            { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0 },
            { 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
            { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };

        public static Dictionary<(int, int), byte> Level58SolutionIndices = new Dictionary<(int, int), byte>
        {
            { (3,5), 3 },
            { (3,8), 4 },
            { (3,6), 5 },
            { (3,7), 6 },
            { (3,9), 7 },
        };
        public static Predicate<(int x, int y)> Levle58InvalidationImprovement =>
            (boxIndices) => boxIndices.x == 1 || boxIndices.x == 6 || boxIndices.y == 1 || boxIndices.y == 13;

        public static byte[,] Level57 = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, }, //9, 7
            { 0, 0, 1, 2, 0, 0, 0, },
            { 0, 0, 1, 1, 0, 0, 0, },
            { 0, 1, 1, 1, 1, 1, 0, },
            { 0, 1, 5, 6, 4, 1, 0, },
            { 0, 1, 1, 7, 1, 1, 0, },
            { 0, 0, 0, 3, 1, 0, 0, },
            { 0, 0, 0, 1, 1, 0, 0, },
            { 0, 0, 0, 0, 0, 0, 0, },
        };

        public static Dictionary<(int, int), byte> Level57SolutionIndices = new Dictionary<(int, int), byte>
        {
            { (2,3), 3 },
            { (3,3), 7 },
            { (4,3), 6 },
            { (4,4), 4 },
            { (4,2), 5 },

        };
        public static Predicate<(int x, int y)> Levle57InvalidationImprovement =>
            (boxIndices) => boxIndices.x == 1 || boxIndices.x == 7 || boxIndices.y == 1 || boxIndices.y == 5;

        /* Step 195
            Solution: DDRRDDLLUUDDRDDLUU
            RRUULDDUULLLDDRRUD
            LLUURRDRRDLLDDRUULUULL
            DDRLUURRDDUULUURDD
            LLDDRUUDDRRRUULLDURRDDLLULL
            URRUULDDRDDRRUULRDDLLUUDD
            RDDLUURRUULDDUULLLDDRRUD
            LLUURRDRRDLLDDRUULUULLDDRLUURRDDRDDLUURRUUL
            Attempts: 7886502, maxStepsLimits: 0, duplicate: 0
         */

        public static byte[,] NearSolvedLevel58 = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //15, 8
            { 0, 1, 1, 1, 1, 1, 0, 0, 0, 2, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 4, 7, 1, 1, 0 },
            { 0, 0, 1, 0, 1, 3, 5, 1, 1, 1, 1, 0, 1, 0, 0 },
            { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 6, 1, 1, 0 },
            { 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
            { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };

        public static byte[,] Level59 = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //13, 8
            { 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0 },
            { 0, 1, 4, 1, 0, 0, 0, 1, 1, 4, 1, 1, 0 },
            { 0, 0, 2, 1, 1, 5, 1, 1, 1, 0, 7, 0, 0 },
            { 0, 0, 3, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };
        public static Predicate<(int x, int y)> Levle59InvalidationImprovement =>
            (boxIndices) => boxIndices.x == 1
            || boxIndices.x == 6
            || boxIndices.y == 1
            || boxIndices.y == 11
            || (boxIndices.x == 5 && (boxIndices.y == 2 || boxIndices.y == 3));

        public static Dictionary<(int, int), byte> Level59SolutionIndices = new Dictionary<(int, int), byte>
        {
            { (3,3), 3 }, //red
            { (3,4), 5 }, //green
            { (3,5), 6 }, //db
            { (3,6), 4 }, //purple
            { (3,7), 7 }, //blue
        };

        public static byte[,] Level2 = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0,  }, //8, 8
            { 0, 2, 1, 1, 1, 1, 0, 0,  },
            { 0, 1, 0, 1, 0, 3, 0, 0,  },
            { 0, 1, 1, 6, 1, 1, 1, 0,  },
            { 0, 1, 0, 1, 0, 4, 1, 0,  },
            { 0, 1, 5, 1, 7, 1, 1, 0,  },
            { 0, 0, 0, 1, 1, 1, 0, 0,  },
            { 0, 0, 0, 0, 0, 0, 0, 0,  },
        };

        public static Dictionary<(int, int), byte> Level2SolutionIndices = new Dictionary<(int, int), byte>
        {
            {(2,3), 3 },
            {(3,4), 4 },
            {(3,2), 5 },
            {(3,3), 6 },
            {(4,3), 7 },
        };

        public static byte[,] Level21 = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0,  }, //7, 8
            { 0, 1, 1, 1, 2, 1, 0, 0,  },
            { 0, 1, 1, 1, 4, 1, 1, 0,  },
            { 0, 0, 1, 5, 6, 3, 1, 0,  },
            { 0, 0, 1, 1, 7, 1, 1, 0,  },
            { 0, 0, 0, 0, 1, 1, 0, 0,  },
            { 0, 0, 0, 0, 0, 0, 0, 0,  },

        }; //1, 4 Hero

        public static Dictionary<(int, int), byte> Level21SolutionIndices = new Dictionary<(int, int), byte>
        {
            { (2,3), 3 }, //red
            { (2,5), 5 }, //green
            { (4,3), 4 }, //purple
            { (4,5), 7 }, //blue
            { (3,4), 6 }, //db
        };

        public static byte Wall = 0;
        public static byte EmptyTile = 1;
        public static byte HeroTile = 2;

        //0 = wall
        //1 = empty
        //2 = hero

        //3 = red
        //4 = purple 
        //5 = green
        //6 = dark blue
        //7 = blue
    }

    public class LegacyGridLayouts
    {
        public static byte[,] Level1 = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0 }, //7, 7
            { 0, 1, 1, 1, 1, 1, 0 },
            { 0, 3, 3, 3, 3, 3, 0 },
            { 0, 1, 1, 2, 1, 1, 0 },
            { 0, 3, 3, 3, 3, 3, 0 },
            { 0, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };

        public static Dictionary<(int, int), byte> Level1SolutionIndices = new Dictionary<(int, int), byte>()
        {
            { (1, 1), 3 },
            { (1, 2), 3 },
            { (1, 3), 3 },
            { (1, 4), 3 },
            { (1, 5), 3 },
            { (5, 1), 3 },
            { (5, 2), 3 },
            { (5, 3), 3 },
            { (5, 4), 3 },
            { (5, 5), 3 },
        };

        //0 = wall
        //1 = empty
        //2 = hero
        //3 = any box
    }
}
