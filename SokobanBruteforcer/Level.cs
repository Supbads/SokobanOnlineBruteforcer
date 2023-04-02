using System.Text;
using System.Security.Cryptography;
using SokobanBruteforcer.Interfaces;

namespace SokobanBruteforcer
{
    public class Level : IHashable, IGridable
    {
        public static Dictionary<(int x, int y), byte> _solutions;
        public static SHA1 sha1  = new SHA1CryptoServiceProvider();
        public static MD5 md5 = new MD5CryptoServiceProvider();
        private static StringBuilder sb;

        //Initial level constructor
        public Level(byte[,] grid, Level previousLevel, short stepsCount)
        {
            sb = new StringBuilder(SolutionVariables._xLength * SolutionVariables._yLength);
            Grid = grid;
            PreviousLevel = previousLevel;
            StepsCount = stepsCount;
            Pushed = false;
            IncomingDirection = Direction.None;
        }

        public Level(byte[,] level, (int, int) heroIndex, Level previousLevel)
        {
            if(previousLevel != null)
            {
                StepsCount = (short)(previousLevel.StepsCount + 1);
            }
            Grid = level;
            PreviousLevel = previousLevel;
        }

        public Level(byte[,] level, (int, int) heroIndex, Level previousLevel, bool pushed, Direction incomingDirection)
        {
            if (previousLevel != null)
            {
                StepsCount = (short)(previousLevel.StepsCount + 1);
            }
            Grid = level;
            PreviousLevel = previousLevel;
            Pushed = pushed;
            IncomingDirection = incomingDirection;
        }

        public byte[,] Grid { get; set; }
        public Level PreviousLevel { get; set; }
        public short StepsCount { get; set; }
        public bool Pushed { get; }
        public Direction IncomingDirection { get; }

        public (int x, int y) FindHeroIndex()
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if(Grid[i, j] == GridLayouts.HeroTile)
                    {
                        return (i, j);
                    }
                }
            }

            throw new Exception("no hero index found");
        }

        public static (int, int) FindHeroIndexStatic(byte[,] grid)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == GridLayouts.HeroTile)
                    {
                        return (i, j);
                    }
                }
            }

            throw new Exception("no hero index found");
        }

        public bool IsLevelSolved()
        {
            foreach (var solution in _solutions)
            {
                if (this.Grid[solution.Key.Item1, solution.Key.Item2] != solution.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public byte[,] GetGrid()
        {
            return Grid;
        }

        public string GetHash()
        {
            return GenerateSnapshotGrid(Grid);
        }

        public string GenerateSnapshot()
        {
            return GenerateSnapshotGrid(Grid);
        }

        public static string GenerateSnapshotGrid(byte[,] grid)
        {
            //return GenerateSnapshotOld(grid);
            //return GenerateSnapshot(grid);
            return GenerateSnapshotV2(grid);
            //return GenerateSnapshotV4(grid);
        }

        public static string GenerateSnapshotV2(byte[,] grid)
        {
            unchecked
            {
                long hash = 17;

                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if(grid[i, j] != 0)
                        {
                            hash = hash * 23 + Convert.ToInt64(Math.Pow(grid[i, j], i));
                            hash = hash * 23 + Convert.ToInt64(Math.Pow(grid[i, j], j));                            
                        }
                    }
                }

                return hash.ToString();
            }
        }

        
        public static string GenerateSnapshotV4(byte[,] grid)
        {
            sb.Clear();
            for (int i = 0; i < SolutionVariables._xLength; i++)
            {
                for (int j = 0; j < SolutionVariables._yLength; j++)
                {
                    sb.Append(grid[i, j].ToString());
                }
            }

            return sb.ToString().GetHashCode().ToString();
        }

        public static string GenerateSnapshotOld(byte[,] grid)
        {
            var lookForObjects = _solutions.Values.ToHashSet();
            lookForObjects.Add(GridLayouts.HeroTile);
            //lookForObjects.Add(GridLayouts.HoleBlock);
            List<(byte obj, int x, int y)> saughtObjectsCoordinates = new List<(byte, int, int)>(_solutions.Keys.Count);

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (lookForObjects.Contains(grid[i, j]))
                    {
                        saughtObjectsCoordinates.Add((grid[i, j], i, j));
                    }
                }
            }

            var snapshot = string.Join("", saughtObjectsCoordinates
                .OrderBy(so => so.obj).ThenBy(so => so.x).ThenBy(so => so.y)
                .Select(so => $"{so.x},{so.y}|")
                .ToArray());

            return snapshot;
        }

        //public static byte[,] GenerateSnapshotByteArray(byte[,] grid)
        //{
        //    var lookForObjects = _solutions.Values.ToHashSet();
        //    lookForObjects.Add(GridLayouts.HeroTile);
        //    List<(byte obj, int x, int y)> saughtObjectsCoordinates = new List<(byte, int, int)>(_solutions.Keys.Count);

        //    for (int i = 0; i < grid.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < grid.GetLength(1); j++)
        //        {
        //            if (lookForObjects.Contains(grid[i, j]))
        //            {
        //                saughtObjectsCoordinates.Add((grid[i, j], i, j));
        //            }
        //        }
        //    }

        //    var snapshot = string.Join("", saughtObjectsCoordinates
        //        .OrderBy(so => so.obj).ThenBy(so => so.x).ThenBy(so => so.y)
        //        .Select(so => $"({so.x},{so.y})")
        //        .ToArray());

        //    return snapshot;
        //}

        public static string GenerateSnapshot(byte[,] grid)
        {
            var buffer = new byte[SolutionVariables._xLength * SolutionVariables._yLength];
            for (int i = 0; i < SolutionVariables._xLength; i++)
            {
                for (int j = 0; j < SolutionVariables._yLength; j++)
                {
                    buffer[(SolutionVariables._xLength * i) + j] = grid[i, j];
                }
            }

            //return string.Concat(md5.ComputeHash(buffer).Select(x => x.ToString("X2")));
            return string.Concat(sha1.ComputeHash(buffer).Select(x => x.ToString("X2")));
        }

        public List<(byte[,] grid, short step)> GetGridsChain(bool reverse = true)
        {
            List<(byte[,] grid, short step)> grids = new List<(byte[,], short)>(StepsCount != int.MaxValue ? StepsCount : 1);
            
            grids.Add((Grid, StepsCount));
            var currentLvl = PreviousLevel;
            while (currentLvl != null)
            {
                grids.Add((currentLvl.Grid, currentLvl.StepsCount));
                currentLvl = currentLvl.PreviousLevel;
            }

            if(reverse)
                grids.Reverse();

            return grids;
        }

        public List<Level> GetLevelsChain(bool reverse = true)
        {
            List<Level> levels = new List<Level>(StepsCount);
            levels.Add(this);
            var prevLevel = PreviousLevel;
            while(prevLevel != null)
            {
                levels.Add(prevLevel);
                prevLevel = prevLevel.PreviousLevel;
            }

            if(reverse)
                levels.Reverse();

            return levels;
        }


        public void PrintSolutionsChain()
        {
            var gridsChain = GetGridsChain();
            var stepCount = 0;
            foreach (var grid in gridsChain)
            {
                Console.WriteLine($"Step {grid.step}");
                PrintLevel(grid.grid);
                stepCount += 1;
            }
        }

        
        public void PrintHeroSteps()
        {
            var levels = GetLevelsChain();
            StringBuilder sb = new StringBuilder(StepsCount + 1);
            for (int i = 0; i < levels.Count - 1; i++)
            {
                var currentHeroIndex = levels[i].FindHeroIndex();
                var nextHeroIndex = levels[i + 1].FindHeroIndex();

                if (currentHeroIndex.x < nextHeroIndex.x)
                {
                    sb.Append("D");
                }
                else if (currentHeroIndex.x > nextHeroIndex.x)
                {
                    sb.Append("U");
                }
                else if (currentHeroIndex.y < nextHeroIndex.y)
                {
                    sb.Append("R");
                }
                else if (currentHeroIndex.y > nextHeroIndex.y)
                {
                    sb.Append("L");
                }
                else
                {
                    sb.Append("Error");
                }
            }
            
            Console.WriteLine($"Solution: {sb.ToString()}");
        }

        static void PrintLevel(byte[,] level)
        {
            Console.WriteLine("Printing Level");

            var xLen = level.GetLength(0);
            var yLen = level.GetLength(1);
            for (int i = 0; i < xLen; i++)
            {
                byte[] col = new byte[yLen];
                for (int j = 0; j < yLen; j++)
                {
                    col[j] = level[i, j];
                }

                Console.WriteLine(string.Join(",", col));
            }

            Console.WriteLine();
            Console.WriteLine("End");
            Console.WriteLine();
        }

        public (byte solvedItemsCount, List<byte> solvedItems) GetSolvedItemsSnapshot()
        {
            List<byte> solvedItems = new List<byte>(6);

            foreach (var solution in _solutions)
            {
                if (Grid[solution.Key.Item1, solution.Key.Item2] == solution.Value)
                {
                    solvedItems.Add(solution.Value);
                }
            }

            return ((byte)solvedItems.Count, solvedItems);
        }
    }
}
