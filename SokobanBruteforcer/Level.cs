﻿using System.Text;

namespace SokobanBruteforcer
{
    public class Level : ILevel
    {
        public static Dictionary<(int, int), byte> _solutions;
        
        public Level(byte[,] grid, ILevel previousLevel, short stepsCount)
        {
            Grid = grid;
            PreviousLevel = previousLevel;
            StepsCount = stepsCount;
            HeroIndex = FindHeroIndex();
            IsSolved = IsLevelSolved(Grid);
        }

        public Level(byte[,] level, (int, int) heroIndex, ILevel previousLevel)
        {
            if(previousLevel != null)
            {
                StepsCount = (short)(previousLevel.StepsCount + 1);
            }
            Grid = level;
            HeroIndex = heroIndex;
            PreviousLevel = previousLevel;
            IsSolved = IsLevelSolved(Grid);
        }

        public Level(byte[,] level, (int, int) heroIndex, ILevel previousLevel, bool pushed, Direction incomingDirection)
        {
            if (previousLevel != null)
            {
                StepsCount = (short)(previousLevel.StepsCount + 1);
            }
            Grid = level;
            HeroIndex = heroIndex;
            PreviousLevel = previousLevel;
            IsSolved = IsLevelSolved(Grid);

            //todo optimize add if we did not push and the direction is X, don't solve the opposite direction as it has been visited
            
        }

        public bool IsSolved { get; }
        public byte[,] Grid { get; set; }
        public ILevel PreviousLevel { get; set; }
        public short StepsCount { get; set; }
        public (int x, int y) HeroIndex { get; set; }

        public (int, int) FindHeroIndex()
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

        private bool IsLevelSolved(byte[,] level)
        {
            foreach (var solution in _solutions)
            {
                if (level[solution.Key.Item1, solution.Key.Item2] != solution.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public string GenerateSnapshot()
        {
            return GenerateSnapshotV2(Grid);
            //return GenerateSnapshot(Grid);
        }

        public static string GenerateSnapshotV2(byte[,] grid)
        {
            string response = String.Empty;

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

                response = hash.ToString();
            }

            return response;
        }

        public byte GetSolvedItemsCount()
        {
            byte solvedItemsCount = 0;

            foreach (var solution in _solutions)
            {
                if (Grid[solution.Key.Item1, solution.Key.Item2] == solution.Value)
                {
                    solvedItemsCount += 1;
                }
            }

            return solvedItemsCount;
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

        public static string GenerateSnapshot(byte[,] lvl)
        {
            var lookForObjects = _solutions.Values.ToHashSet();
            lookForObjects.Add(GridLayouts.HeroTile);
            List<(byte obj, int x, int y)> saughtObjectsCoordinates = new List<(byte, int, int)>(_solutions.Keys.Count);

            for (int i = 0; i < lvl.GetLength(0); i++)
            {
                for (int j = 0; j < lvl.GetLength(1); j++)
                {
                    if (lookForObjects.Contains(lvl[i, j]))
                    {
                        saughtObjectsCoordinates.Add((lvl[i, j], i, j));
                    }
                }
            }

            var snapshot = string.Join("", saughtObjectsCoordinates
                .OrderBy(so => so.obj).ThenBy(so => so.x).ThenBy(so => so.y)
                .Select(so => $"({so.x},{so.y})")
                .ToArray());

            return snapshot;
        }

        public List<byte[,]> GetGridsChain(bool reverse = true)
        {
            List<byte[,]> grids = new List<byte[,]>(StepsCount != int.MaxValue ? StepsCount : 1);
            
            grids.Add(Grid);
            var currentLvl = PreviousLevel;
            while (currentLvl != null)
            {
                grids.Add(currentLvl.Grid);
                currentLvl = currentLvl.PreviousLevel;
            }

            if(reverse)
                grids.Reverse();

            return grids;
        }

        public List<ILevel> GetLevelsChain(bool reverse = true)
        {
            List<ILevel> levels = new List<ILevel>(StepsCount);
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
                PrintLevel(grid);
                Console.WriteLine($"Step {stepCount}");
                stepCount += 1;
            }
        }

        
        public void PrintHeroSteps()
        {
            var levels = GetLevelsChain();
            StringBuilder sb = new StringBuilder(StepsCount + 1);
            for (int i = 0; i < levels.Count - 1; i++)
            {
                var currentHeroIndex = levels[i].HeroIndex;
                var nextHeroIndex = levels[i + 1].HeroIndex;

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
    }
}