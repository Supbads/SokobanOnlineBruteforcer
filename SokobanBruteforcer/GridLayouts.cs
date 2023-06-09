﻿namespace SokobanBruteforcer
{
    public class GridLayouts
    {
        //248, 252
        public static byte[,] Level58 = new byte[,] // https://www.sokobanonline.com/play/community/mikearcher/5-boxes/131408_puzzle-58
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

       //public static byte[,] Level58TrimTest = new byte[,]
       //{            
       //     { 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, },
       //     { 1, 1, 4, 1, 1, 0, 0, 0, 1, 1, 5, 1, 1, },
       //     { 0, 3, 0, 2, 1, 1, 6, 1, 1, 1, 0, 7, 0, },
       //     { 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, },
       //     { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, },
       //     { 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, },          
       //};

        public static Dictionary<(int, int), byte> Level58SolutionIndices = new Dictionary<(int, int), byte>
        {
            { (3,5), 3 },
            { (3,8), 4 },
            { (3,6), 5 },
            { (3,7), 6 },
            { (3,9), 7 },
        };
        public static Predicate<(int x, int y)> Level58InvalidationImprovement =>
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
        public static Predicate<(int x, int y)> Level57InvalidationImprovement =>
            (boxIndices) => boxIndices.x == 1 || boxIndices.x == 7 || boxIndices.y == 1 || boxIndices.y == 5;

        /* Step 195
            Solution: DDRRDDLLUUDDRDDLUURRUULDDUULLLDDRRUDLLUURRDRRDLLDDRUULUULLDDRLUURRDDUULUURDDLLDDRUUDDRRRUULLDURRDDLLULLURRUULDDRDDRRUULRDDLLUUDDRDDLUURRUULDDUULLLDDRRUDLLUURRDRRDLLDDRUULUULLDDRLUURRDDRDDLUURRUUL
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
        public static Predicate<(int x, int y)> Level59InvalidationImprovement =>
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

        public static byte[,] Level19 = new byte[,] //https://www.sokobanonline.com/play/community/mikearcher/5-boxes/128187_puzzle-19
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
        //182 steps 
        //RRDDRRRDDLDLLUUULLDDRLUURRRRRDDLDLLUUDDRRURUULLLDDLLUUUURRRRDULLLLDDDDRRDRRURUULRDDLUUDDDLLURLLLUUUURRDDDUUULLDDDDRRDRRURUULLRRDDLDLLUUURRRDDLLDLUUDRRRUULDRDLLDLULLUUUURRRRDDRDDLURUL
        public static Predicate<(int x, int y)> Level19InvalidationImprovement =>
            (boxIndices) => boxIndices.x == 1 || boxIndices.x == 6 || boxIndices.y == 1 || boxIndices.y == 6;

        public static Dictionary<(int, int), byte> Level19SolutionIndices = new Dictionary<(int, int), byte>
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

        //Steps 89
        //Solution: RDRDDLLRRUULLDURRDDLURULULLLDDRRLLUURDURRDLLRRRDDLDLURURULULLDRLULLDRDDRRLLUUURRDLDUULLDR

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

        public static byte Hole = 8;
        public static byte HoleBlock = 9;

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

    public class EmptyHolesTest
    {
        public static byte[,] LevelHolyHow4 = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //15, 18
            { 0, 8, 1, 1, 8, 8, 8, 8, 8, 9, 8, 8, 8, 8, 8, 8, 8, 0 },
            { 0, 8, 1, 1, 8, 8, 8, 8, 9, 8, 9, 8, 8, 8, 8, 8, 8, 0 },
            { 0, 8, 1, 9, 9, 8, 8, 8, 8, 9, 8, 8, 8, 8, 8, 8, 8, 0 },
            { 0, 8, 8, 8, 9, 8, 8, 8, 8, 8, 9, 9, 8, 8, 8, 8, 8, 0 },
            { 0, 8, 1, 9, 8, 9, 8, 9, 8, 8, 8, 9, 8, 8, 8, 8, 8, 0 },
            { 0, 8, 1, 1, 8, 8, 8, 8, 9, 8, 9, 8, 8, 8, 8, 8, 8, 0 },
            { 0, 2, 0, 1, 9, 9, 8, 9, 8, 8, 8, 9, 8, 9, 8, 8, 8, 0 },
            { 0, 9, 8, 9, 9, 8, 8, 8, 8, 8, 9, 8, 8, 8, 9, 8, 8, 0 },
            { 0, 8, 8, 8, 8, 8, 8, 9, 8, 9, 8, 8, 9, 8, 8, 8, 8, 0 },
            { 0, 9, 8, 9, 9, 8, 9, 8, 8, 9, 8, 8, 8, 8, 9, 8, 8, 0 },
            { 0, 8, 8, 8, 8, 8, 8, 8, 8, 8, 9, 9, 8, 9, 8, 8, 8, 0 },
            { 0, 9, 8, 9, 9, 8, 9, 8, 8, 9, 9, 8, 8, 8, 8, 8, 8, 0 },
            { 0, 8, 9, 8, 8, 8, 8, 8, 8, 8, 8, 8, 1, 5, 1, 8, 8, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            //                                       ^g ^ge (13,14)
        };

        public static Dictionary<(int, int), byte> LevelHolyHow4Solutions = new Dictionary<(int, int), byte>
        {
            { (13, 14), 5 },
        };

        //0 = wall
        //1 = empty
        //2 = hero

        //3 = red
        //4 = purple 
        //5 = green
        //6 = dark blue
        //7 = blue

        //8 = hole
        //9 = holeBlock


    }

    public class MicrobanLayout
    {
        public static byte[,] Level69 = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, //10, 9
            { 0, 1, 1, 1, 1, 1, 1, 0, 0 },
            { 0, 1, 0, 0, 0, 0, 1, 0, 0 },
            { 0, 1, 0, 1, 1, 1, 2, 0, 0 },
            { 0, 1, 0, 0, 0, 3, 0, 0, 0 },
            { 0, 1, 0, 1, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 3, 3, 1, 3, 1, 0 },
            { 0, 0, 0, 0, 1, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        }; //https://www.sokobanonline.com/play/web-archive/david-w-skinner/microban/821_microban-61

        public static Dictionary<(int, int), byte> Level69SolutionIndices = new Dictionary<(int, int), byte>()
        {
            { (3, 3), 3 },
            { (3, 4), 3 },
            { (3, 5), 3 },
            { (8, 4), 3 },
        };

        public static Predicate<(int x, int y)> Level69InvalidationImprovement =>
            (boxIndices) => boxIndices.y == 1 || boxIndices.y == 7;


    }
}
