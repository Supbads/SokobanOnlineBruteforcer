using System.Diagnostics;

namespace SokobanBruteforcer
{
    public class SokobanSolver
    {
        /// <summary>
        /// The main function to solve a level
        /// </summary>
        /// <param name="initialLevel">The initial state of the level that will be solved</param>
        /// <param name="useStringSnapshotting">The snapshotting pattern that will be used: Raw string snapshotting or Byte Matrix Equality Comparison</param>
        /// <param name="maxSteps">Maximum number of steps after which the solutions will be discarded</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool SolveSokobanLevel(Level initialLevel, bool useStringSnapshotting, int maxSteps = 120)
        {
            SolutionVariables._currentSolutions = Level._solutions;
            SolutionVariables.useStringSnapshotting = useStringSnapshotting;

            foreach (var solution in Level._solutions)
            {
                if (initialLevel.Grid[solution.Key.x, solution.Key.y] == 0)
                {
                    throw new Exception("Finishing tile is a wall. Verify setup");
                }
            }

            SolutionVariables._xLength = initialLevel.Grid.GetLength(0);
            SolutionVariables._yLength = initialLevel.Grid.GetLength(1);

            SolutionVariables._levelsToBruteforce = new IndexedQueue<Level>(200000);
            SolutionVariables._levelsToBruteforce.Enqueue(initialLevel);
            //add next potential steps to a queue or stack
            SolutionVariables._visitedLevelsSnapshots = new Dictionary<string, short>(10000);
            SolutionVariables._pendingLevelsSnapshots = new Dictionary<string, short>(10000);
            SolutionVariables._visitedLevelsSnapshotsByte = new Dictionary<byte[,], short>(500000, new ByteArrayComparer());
            SolutionVariables._pendingLevelsSnapshotsByte = new Dictionary<byte[,], short>(500000, new ByteArrayComparer());

            HashSet<string> duplicateSnapshots = new HashSet<string>(100);

            int maxStepsLimitReached = 0;
            long attempts = 0;
            int duplicate = 0;
            bool manualMode = false;
            var sw = Stopwatch.StartNew();

            //bool print = false;
            //var biggestStepsCount = 0;

            if (manualMode)
            {
                while (SolutionVariables._levelsToBruteforce.Any())
                {
                    var level = SolutionVariables._levelsToBruteforce.Dequeue();
                    if (level.IsLevelSolved())
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

                    if (level.IsLevelSolved())
                    {
                        SolutionVariables.foundSolution = true;
                        Console.WriteLine("Solution Found");
                        if (level.StepsCount < SolutionVariables._bestSteps)
                        {
                            Console.WriteLine($"New best Solution Found. Steps: {level.StepsCount}");
                            Console.WriteLine($"Time: {TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds).ToString("mm\\:ss\\:FFFF")}");
                            Console.WriteLine($"Solution found in {attempts} attempts");

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
                    
                    if (SolutionVariables.foundSolution && SolutionVariables._bestSteps < level.StepsCount)
                    {
                        continue;
                    }

                    //if(level.StepsCount > biggestStepsCount)
                    //{
                    //    biggestStepsCount = level.StepsCount;
                    //}

                    if (level.StepsCount > maxSteps)
                    {
                        maxStepsLimitReached++;
                        continue;
                    }

                    //if (print)
                    //{
                    //    PrintLevel(level.Grid);
                    //}

                    #region legacySnapshotting
                    if (SolutionVariables.useStringSnapshotting)
                    {
                        var snp = level.GenerateSnapshot();
                        if (SolutionVariables._visitedLevelsSnapshots.ContainsKey(snp) && SolutionVariables._visitedLevelsSnapshots[snp] < level.StepsCount)
                        {
                            if (duplicateSnapshots.Contains(snp))
                            {
                                Console.WriteLine($"Already contained snp: {snp}");
                            }
                            duplicateSnapshots.Add(snp);

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
            Console.WriteLine($"Algorithm ended in {TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds).ToString("mm\\:ss\\:FFFF")} ms");
            
            if (SolutionVariables._bestLevel != null)
            {
                SolutionVariables._bestLevel.PrintSolutionsChain();

                SolutionVariables._bestLevel.PrintHeroSteps();
                Console.WriteLine($"Algorithm ended in {TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds).ToString("mm\\:ss\\:FFFF")} ms");
            }
            else
            {
                Console.WriteLine($"Algorithm ended in {TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds).ToString("mm\\:ss\\:FFFF")}");
                Console.WriteLine("No solution found");
            }

            Console.WriteLine($"Attempts: {attempts}, maxStepsLimits: {maxStepsLimitReached}, duplicate: {duplicate}"); //remove duplicates potentially ?
            Console.WriteLine($"WallChecksExcludes: {SokobanMovement.wallChecksFuncInvalidations}, excludedSolutions: {SokobanMovement.excludedSolutions}");
            Console.WriteLine($"WallChecksExcludesHacks: {SokobanMovement.wallChecksFuncInvalidationsHack}, excludedSolutionsQuadro: {SokobanMovement.wallChecksFuncInvalidationsQuadro}");
            Console.WriteLine($"EqualsChecks: {ByteArrayComparer.EqualsChecks}, Hashes: {ByteArrayComparer.HashCodes}");
            Console.WriteLine($"ImprovementLogicCount: {SokobanMovementQueueUpOnlyPushes.SolveWhileQueueingOnlyMovementsCount}");
            Console.WriteLine($"MovementsDequeueCount: {SokobanMovementQueueUpOnlyPushes.SolveWhileQueueingOnlyMovementsDequeueCount}");
            Console.WriteLine($"MovementsProcessedCount: {SokobanMovementQueueUpOnlyPushes.SolveWhileQueueingOnlyMovementsProcessedCount}");
            
            return SolutionVariables._bestLevel != null;
        }

        static bool SolveManualMode(Level level)
        {
            var heroIndex = level.FindHeroIndex();
            PrintLevel(level.Grid);
            Console.Write("Input Key: ");
            var key = Console.ReadKey().Key;
            Console.WriteLine($"{key.ToString()}");

            if (key == ConsoleKey.LeftArrow)
            {
                SokobanMovement.TryMoveLeft(level, heroIndex, true);
            }
            else if (key == ConsoleKey.UpArrow)
            {
                SokobanMovement.TryMoveUp(level, heroIndex, true);
            }
            else if (key == ConsoleKey.DownArrow)
            {
                SokobanMovement.TryMoveDown(level, heroIndex, true);
            }
            else if (key == ConsoleKey.RightArrow)
            {
                SokobanMovement.TryMoveRight(level, heroIndex, true);
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
            SokobanMovementQueueUpOnlyPushes.SolveWhileQueueingOnlyMovements(level);
            return;

            var heroIndex = level.FindHeroIndex();
            if (level.Pushed)
            {
                SokobanMovement.TryMoveLeft(level, heroIndex);
                SokobanMovement.TryMoveUp(level, heroIndex);
                SokobanMovement.TryMoveRight(level, heroIndex);
                SokobanMovement.TryMoveDown(level, heroIndex);
            }
            else
            {
                switch (level.IncomingDirection)
                {
                    case Direction.Up:
                        SokobanMovement.TryMoveLeft(level, heroIndex);
                        SokobanMovement.TryMoveUp(level, heroIndex);
                        SokobanMovement.TryMoveRight(level, heroIndex);
                        break;
                    case Direction.Left:
                        SokobanMovement.TryMoveLeft(level, heroIndex);
                        SokobanMovement.TryMoveUp(level, heroIndex);
                        SokobanMovement.TryMoveDown(level, heroIndex);
                        break;
                    case Direction.Right:
                        SokobanMovement.TryMoveUp(level, heroIndex);
                        SokobanMovement.TryMoveRight(level, heroIndex);
                        SokobanMovement.TryMoveDown(level, heroIndex);
                        break;
                    case Direction.Down:
                        SokobanMovement.TryMoveLeft(level, heroIndex);
                        SokobanMovement.TryMoveRight(level, heroIndex);
                        SokobanMovement.TryMoveDown(level, heroIndex);
                        break;
                    case Direction.None:
                        SokobanMovement.TryMoveLeft(level, heroIndex);
                        SokobanMovement.TryMoveUp(level, heroIndex);
                        SokobanMovement.TryMoveRight(level, heroIndex);
                        SokobanMovement.TryMoveDown(level, heroIndex);
                        break;
                    default:
                        throw new Exception("Shouldn't be hit");
                }
            }
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
