using System.Diagnostics;

namespace SokobanBruteforcer
{
    public class SokobanSolver
    {
        public static bool SolveSokobanLevel(Level initialLevel)
        {
            SolutionVariables._currentSolutions = Level._solutions;
            SolutionVariables.useStringSnapshotting = false;

            foreach (var solution in Level._solutions)
            {
                if (initialLevel.Grid[solution.Key.x, solution.Key.y] == 0)
                {
                    throw new Exception("Finishing tile is a wall. Verify setup");
                }
            }


            SolutionVariables._xLength = initialLevel.Grid.GetLength(0);
            SolutionVariables._yLength = initialLevel.Grid.GetLength(1);

            SolutionVariables._levelsToBruteforce = new Queue<Level>(5000);
            SolutionVariables._levelsToBruteforce.Enqueue(initialLevel);
            //add next potential steps to a queue or stack
            SolutionVariables._visitedLevelsSnapshots = new Dictionary<string, short>(3000);
            SolutionVariables._pendingLevelsSnapshots = new Dictionary<string, short>(3000);
            SolutionVariables._visitedLevelsSnapshotsByte = new Dictionary<byte[,], short>(3000, new ByteArrayComparer());
            SolutionVariables._pendingLevelsSnapshotsByte = new Dictionary<byte[,], short>(3000, new ByteArrayComparer());

            int maxStepsLimitReached = 0;
            int attempts = 0;
            int duplicate = 0;
            bool print = false;
            bool manualMode = false;
            int maxSteps = 120;
            var sw = Stopwatch.StartNew();


            if (manualMode)
            {
                while (SolutionVariables._levelsToBruteforce.Any())
                {
                    var level = SolutionVariables._levelsToBruteforce.Dequeue();
                    if (level.IsSolved)
                    {
                        Console.WriteLine($"Level was solved in steps {attempts}");
                    }

                    bool incrementSteps = SolveManualMode(level);
                    if (incrementSteps)
                        attempts++;
                }
            }
            else
            {
                while (SolutionVariables._levelsToBruteforce.Any())
                {
                    var level = SolutionVariables._levelsToBruteforce.Dequeue();

                    if (level.IsSolved)
                    {
                        SolutionVariables.foundSolution = true;
                        Console.WriteLine("Solution Found");
                        if (level.StepsCount < SolutionVariables._bestSteps)
                        {
                            Console.WriteLine($"New best Solution Found. Steps: {level.StepsCount}");
                            //PrintLevel(level.Grid);
                            SolutionVariables._bestSteps = level.StepsCount;
                            SolutionVariables._bestLevel = level;
                        }
                        else
                        {
                            Console.WriteLine($"Solution not optimal. Steps: {level.StepsCount} skpping.");
                        }
                        continue;
                    }
                    if (print)
                    {
                        PrintLevel(level.Grid);
                    }

                    if (SolutionVariables.foundSolution && SolutionVariables._bestSteps < level.StepsCount)
                    {
                        continue;
                    }

                    if (level.StepsCount > maxSteps)
                    {
                        maxStepsLimitReached++;
                        continue;
                    }

                    //PrintLevel(level._level);

                    #region legacySnapshotting
                    if (SolutionVariables.useStringSnapshotting)
                    {
                        var snp = level.GenerateSnapshot();
                        if (SolutionVariables._visitedLevelsSnapshots.ContainsKey(snp) && SolutionVariables._visitedLevelsSnapshots[snp] < level.StepsCount)
                        {
                            duplicate += 1;
                            if (duplicate % 50000 == 0)
                            {
                                Console.WriteLine($"Duplicate {duplicate}");
                            }
                            continue;
                        }
                        else
                        {
                            if (SolutionVariables._visitedLevelsSnapshots.ContainsKey(snp))
                            {
                                SolutionVariables._visitedLevelsSnapshots[snp] = level.StepsCount;
                            }
                            else
                            {
                                SolutionVariables._visitedLevelsSnapshots.Add(snp, level.StepsCount);
                            }

                            SolutionVariables._pendingLevelsSnapshots.Remove(snp);
                        }

                    }
                    #endregion
                    #region byteArraySnapshotting
                    else
                    {
                        if (SolutionVariables._visitedLevelsSnapshotsByte.ContainsKey(level.Grid) && SolutionVariables._visitedLevelsSnapshotsByte[level.Grid] < level.StepsCount)
                        {
                            duplicate += 1;
                            if (duplicate % 50000 == 0)
                            {
                                Console.WriteLine($"Duplicate {duplicate}");
                            }
                            continue;
                        }
                        else
                        {
                            if (SolutionVariables._visitedLevelsSnapshotsByte.ContainsKey(level.Grid))
                            {
                                SolutionVariables._visitedLevelsSnapshotsByte[level.Grid] = level.StepsCount;
                            }
                            else
                            {
                                SolutionVariables._visitedLevelsSnapshotsByte.Add(level.Grid, level.StepsCount);
                            }

                            SolutionVariables._pendingLevelsSnapshotsByte.Remove(level.Grid);
                        }
                    }
                    #endregion

                    attempts += 1;
                    if (attempts % 100000 == 0)
                    {
                        //if(attempts > 2_000_000)
                        //{
                        //PrintLevel(level.Grid);
                        //}
                        Console.WriteLine($"Attempt {attempts}");
                        //PrintLevel(level._level);
                    }

                    Solve(level);
                }
            }

            sw.Stop();
            Console.WriteLine($"Algorithm ended in {TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds).ToString("mm\\:ss\\:FF")}");
            
            if (SolutionVariables._bestLevel != null)
            {
                SolutionVariables._bestLevel.PrintSolutionsChain();

                SolutionVariables._bestLevel.PrintHeroSteps();
                Console.WriteLine($"Algorithm ended in {TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds).ToString("mm\\:ss\\:FF")}");
            }
            else
            {
                Console.WriteLine($"Algorithm ended in {TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds).ToString("mm\\:ss\\:FF")}");
                Console.WriteLine("No solution found");
            }

            Console.WriteLine($"Attempts: {attempts}, maxStepsLimits: {maxStepsLimitReached}, duplicate: {duplicate}"); //remove duplicates potentially ?
            Console.WriteLine($"WallChecksExcludes: {SokobanMovement.wallChecksFuncInvalidations}, excludedSolutions: {SokobanMovement.excludedSolutions}");
            Console.WriteLine($"WallChecksExcludesHacks: {SokobanMovement.wallChecksFuncInvalidationsHack}, excludedSolutionsQuadro: {SokobanMovement.wallChecksFuncInvalidationsQuadro}");
            Console.WriteLine($"EqualsChecks: {ByteArrayComparer.EqualsChecks}, Hashes: {ByteArrayComparer.HashCodes}");

            return SolutionVariables._bestLevel != null;
        }


        static bool SolveManualMode(Level level)
        {
            PrintLevel(level.Grid);
            Console.Write("Input Key: ");
            var key = Console.ReadKey().Key;
            Console.WriteLine($"{key.ToString()}");

            if (key == ConsoleKey.LeftArrow)
            {
                SokobanMovement.TryMoveLeft(level, true);
            }
            else if (key == ConsoleKey.UpArrow)
            {
                SokobanMovement.TryMoveUp(level, true);
            }
            else if (key == ConsoleKey.DownArrow)
            {
                SokobanMovement.TryMoveDown(level, true);
            }
            else if (key == ConsoleKey.RightArrow)
            {
                SokobanMovement.TryMoveRight(level, true);
            }

            if (!SolutionVariables._levelsToBruteforce.Any())
            {
                Console.WriteLine("Input not possible. Try again");
                SolutionVariables._levelsToBruteforce.Enqueue(level);
                return false;
            }

            return true;
        }

        static void Solve(Level level)
        {
            if (level.Pushed)
            {
                SokobanMovement.TryMoveLeft(level);
                SokobanMovement.TryMoveUp(level);
                SokobanMovement.TryMoveRight(level);
                SokobanMovement.TryMoveDown(level);

            }
            else
            {
                switch (level.IncomingDirection)
                {
                    case Direction.Up:
                        SokobanMovement.TryMoveLeft(level);
                        SokobanMovement.TryMoveUp(level);
                        SokobanMovement.TryMoveRight(level);
                        break;
                    case Direction.Left:
                        SokobanMovement.TryMoveLeft(level);
                        SokobanMovement.TryMoveUp(level);
                        SokobanMovement.TryMoveDown(level);
                        break;
                    case Direction.Right:
                        SokobanMovement.TryMoveUp(level);
                        SokobanMovement.TryMoveRight(level);
                        SokobanMovement.TryMoveDown(level);
                        break;
                    case Direction.Down:
                        SokobanMovement.TryMoveLeft(level);
                        SokobanMovement.TryMoveRight(level);
                        SokobanMovement.TryMoveDown(level);
                        break;
                    case Direction.None:
                        SokobanMovement.TryMoveLeft(level);
                        SokobanMovement.TryMoveUp(level);
                        SokobanMovement.TryMoveRight(level);
                        SokobanMovement.TryMoveDown(level);
                        break;
                    default:
                        throw new Exception("Shouldn't be hit");
                }
            }

            //var heroIndex = level.HeroIndex;

            //try left
            //var x = heroIndex.x;
            //var y = heroIndex.y - 1;
            //if (y >= 0 && level.Grid[x, y] != GridLayouts.Wall && level.Grid[x, y] != GridLayouts.Hole)
            //{
            //    if (level.Grid[x, y] == GridLayouts.EmptyTile)
            //    {
            //        var copyLevel = GridUtils.CopyLevel(level.Grid);
            //        copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
            //        copyLevel[x, y] = GridLayouts.HeroTile;

            //        if (!ExcludeSolution(copyLevel, level.StepsCount))
            //            _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            //    }
            //    else
            //    {
            //        if(y - 1 >=0 && level.Grid[x, y] == GridLayouts.HoleBlock && level.Grid[x, y - 1] == GridLayouts.Hole)
            //        {
            //            var copyLevel = GridUtils.CopyLevel(level.Grid);

            //            copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
            //            copyLevel[x, y - 1] = GridLayouts.EmptyTile;
            //            copyLevel[x, y] = GridLayouts.HeroTile;

            //            if (!ExcludeSolution(copyLevel, level.StepsCount, (x, y - 1), true))
            //                _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            //        }
            //        else if (y - 1 >= 0 && level.Grid[x, y - 1] == GridLayouts.EmptyTile)
            //        {
            //            var copyLevel = GridUtils.CopyLevel(level.Grid);

            //            copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
            //            copyLevel[x, y - 1] = copyLevel[x, y];
            //            copyLevel[x, y] = GridLayouts.HeroTile;

            //            if (!ExcludeSolution(copyLevel, level.StepsCount, (x, y - 1)))
            //                _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            //        }
            //    }
            //}

            //try up
            //x = heroIndex.x - 1;
            //y = heroIndex.y;
            //if (x >= 0 && level.Grid[x, y] != GridLayouts.Wall && level.Grid[x, y] != GridLayouts.Hole)
            //{
            //    if (level.Grid[x, y] == GridLayouts.EmptyTile)
            //    {
            //        var copyLevel = GridUtils.CopyLevel(level.Grid);
            //        copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
            //        copyLevel[x, y] = GridLayouts.HeroTile;

            //        if (!ExcludeSolution(copyLevel, level.StepsCount))
            //            _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            //    }
            //    else
            //    {
            //        if (x - 1 >= 0 && level.Grid[x, y] == GridLayouts.HoleBlock && level.Grid[x - 1, y] == GridLayouts.Hole)
            //        {
            //            var copyLevel = GridUtils.CopyLevel(level.Grid);

            //            copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
            //            copyLevel[x - 1, y] = GridLayouts.EmptyTile;
            //            copyLevel[x, y] = GridLayouts.HeroTile;

            //            if (!ExcludeSolution(copyLevel, level.StepsCount, (x - x, y), true))
            //                _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            //        }
            //        else if (x - 1 >= 0 && level.Grid[x - 1, y] == GridLayouts.EmptyTile)
            //        {
            //            var copyLevel = GridUtils.CopyLevel(level.Grid);

            //            copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
            //            copyLevel[x - 1, y] = copyLevel[x, y];
            //            copyLevel[x, y] = GridLayouts.HeroTile;


            //            if (!ExcludeSolution(copyLevel, level.StepsCount, (x - 1, y)))
            //                _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            //        }
            //    }
            //}

            //try right
            //x = heroIndex.x;
            //y = heroIndex.y + 1;
            //if (y < _yLength && level.Grid[x, y] != GridLayouts.Wall && level.Grid[x, y] != GridLayouts.Hole)
            //{
            //    if (level.Grid[x, y] == GridLayouts.EmptyTile)
            //    {
            //        var copyLevel = GridUtils.CopyLevel(level.Grid);
            //        copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
            //        copyLevel[x, y] = GridLayouts.HeroTile;

            //        if (!ExcludeSolution(copyLevel, level.StepsCount))
            //            _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            //    }
            //    else
            //    {
            //        if (y + 1 >= 0 && level.Grid[x, y] == GridLayouts.HoleBlock && level.Grid[x, y + 1] == GridLayouts.Hole)
            //        {
            //            var copyLevel = GridUtils.CopyLevel(level.Grid);

            //            copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
            //            copyLevel[x, y + 1] = GridLayouts.EmptyTile;
            //            copyLevel[x, y] = GridLayouts.HeroTile;

            //            if (!ExcludeSolution(copyLevel, level.StepsCount, (x, y + 1), true))
            //                _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            //        }
            //        else if (y + 1 < _yLength && level.Grid[x, y + 1] == GridLayouts.EmptyTile)
            //        {
            //            var copyLevel = GridUtils.CopyLevel(level.Grid);
            //            copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
            //            copyLevel[x, y + 1] = copyLevel[x, y];
            //            copyLevel[x, y] = GridLayouts.HeroTile;

            //            if (!ExcludeSolution(copyLevel, level.StepsCount, (x, y + 1)))
            //                _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            //        }
            //    }
            //}

            // try down
            //x = heroIndex.x + 1;
            //y = heroIndex.y;
            //if (x < _xLength && level.Grid[x, y] != GridLayouts.Wall && level.Grid[x, y] != GridLayouts.Hole)
            //{
            //    if (level.Grid[x, y] == GridLayouts.EmptyTile)
            //    {
            //        var copyLevel = GridUtils.CopyLevel(level.Grid);

            //        copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
            //        copyLevel[x, y] = GridLayouts.HeroTile;

            //        if (!ExcludeSolution(copyLevel, level.StepsCount))
            //            _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            //    }
            //    else
            //    {
            //        if (x + 1 >= 0 && level.Grid[x, y] == GridLayouts.HoleBlock && level.Grid[x + 1, y] == GridLayouts.Hole)
            //        {
            //            var copyLevel = GridUtils.CopyLevel(level.Grid);

            //            copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
            //            copyLevel[x + 1, y] = GridLayouts.EmptyTile;
            //            copyLevel[x, y] = GridLayouts.HeroTile;

            //            if (!ExcludeSolution(copyLevel, level.StepsCount, (x + 1, y), true))
            //                _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            //        }
            //        else if (x + 1 < _xLength && level.Grid[x + 1, y] == GridLayouts.EmptyTile)
            //        {
            //            var copyLevel = GridUtils.CopyLevel(level.Grid);
            //            copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
            //            copyLevel[x + 1, y] = copyLevel[x, y];
            //            copyLevel[x, y] = GridLayouts.HeroTile;

            //            if (!ExcludeSolution(copyLevel, level.StepsCount, (x + 1, y)))
            //                _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            //        }
            //    }
            //}
        }

        static void PrintLevel(byte[,] level)
        {
            Console.WriteLine("Printing Level");

            for (int i = 0; i < SolutionVariables._xLength; i++)
            {
                byte[] col = new byte[SolutionVariables._yLength];
                for (int j = 0; j < SolutionVariables._yLength; j++)
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
