// See https://aka.ms/new-console-template for more information
using SokobanBruteforcer;
using System.Diagnostics;

public class Program
{
    private static int _xLength;
    private static int _yLength;
    private static Queue<Level> _levelsToBruteforce; //todo test queue vs stack
    private static int _bestSteps = int.MaxValue;
    private static Level _bestLevel;
    private static Dictionary<string, short> _visitedLevelsSnapshots;
    private static Dictionary<string, short> _pendingLevelsSnapshots;
    private static Dictionary<(int x, int y), byte> _currentSolutions;
    private static bool foundSolution = false;

    //testing
    private static byte highestSolvedItemsCount = 0;
    private static string solvedItemsSnapshot = "";

    public static void Main()
    {
        //_currentSolutions = SokobanJuniorLayouts.SokobanJunior15Solutions;
        //Level._solutions = _currentSolutions;
        //var initialLevel = new Level(SokobanJuniorLayouts.SokobanJunior15, null, 0);
        _currentSolutions = GridLayouts.Level57SolutionIndices;
        Level._solutions = _currentSolutions;
        var initialLevel = new Level(GridLayouts.Level57, null, 0);
        _xLength = initialLevel.Grid.GetLength(0);
        _yLength = initialLevel.Grid.GetLength(1);

        _levelsToBruteforce = new Queue<Level>(5000);
        _levelsToBruteforce.Enqueue(initialLevel);
        //add next potential steps to a queue or stack
        _visitedLevelsSnapshots = new Dictionary<string, short>(3000);
        _pendingLevelsSnapshots = new Dictionary<string, short>(3000);

        int maxStepsLimitReached = 0;
        int attempts = 0;
        int duplicate = 0;
        bool print = false;
        bool manualMode = false;
        int maxSteps = 210;
        var sw = Stopwatch.StartNew();       


        if (manualMode)
        {
            while (_levelsToBruteforce.Any())
            {
                var level = _levelsToBruteforce.Dequeue();
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
            while (_levelsToBruteforce.Any())
            {
                var level = _levelsToBruteforce.Dequeue();

                if (level.IsSolved)
                {
                    foundSolution = true;
                    Console.WriteLine("Solution Found");
                    if (level.StepsCount < _bestSteps)
                    {
                        Console.WriteLine($"New best Solution Found. Steps: {level.StepsCount}");
                        //PrintLevel(level.Grid);
                        _bestSteps = level.StepsCount;
                        _bestLevel = level;
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

                if (foundSolution && _bestSteps < level.StepsCount)
                {
                    continue;
                }

                if (level.StepsCount > maxSteps)
                {
                    maxStepsLimitReached++;
                    continue;
                }

                //PrintLevel(level._level);
                var snp = level.GenerateSnapshot();
                if (_visitedLevelsSnapshots.ContainsKey(snp) && _visitedLevelsSnapshots[snp] < level.StepsCount)
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
                    if (_visitedLevelsSnapshots.ContainsKey(snp))
                    {
                        _visitedLevelsSnapshots[snp] = level.StepsCount;
                    }
                    else
                    {
                        _visitedLevelsSnapshots.Add(snp, level.StepsCount);
                    }

                    _pendingLevelsSnapshots.Remove(snp);
                }

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

        if (_bestLevel != null)
        {
            _bestLevel.PrintSolutionsChain();

            _bestLevel.PrintHeroSteps();
            Console.WriteLine($"Attempts: {attempts}, maxStepsLimits: {maxStepsLimitReached}, duplicate: {duplicate}"); //remove duplicates potentially ?
        }
        else
        {
            Console.WriteLine("No solution found");
            Console.WriteLine($"Attempts: {attempts}, maxStepsLimits: {maxStepsLimitReached}, duplicate: {duplicate}"); //remove duplicates potentially ?
            Console.WriteLine($"WallChecksExcludes: {wallChecksFuncInvalidations}, excludedSolutions: {excludedSolutions}");
            Console.WriteLine($"WallChecksExcludesHacks: {wallChecksFuncInvalidationsHack}, excludedSolutionsQuadro: {wallChecksFuncInvalidationsQuadro}");
        }
    }


    static bool SolveManualMode(Level level)
    {
        PrintLevel(level.Grid);
        var heroIndex = level.HeroIndex;
        Console.Write("Input Key: ");
        var key = Console.ReadKey().Key;
        Console.WriteLine($"{key.ToString()}");

        if (key == ConsoleKey.LeftArrow)
        {
            var x = heroIndex.x;
            var y = heroIndex.y - 1;
            if (y >= 0 && level.Grid[x, y] != GridLayouts.Wall)
            {
                if (level.Grid[x, y] == GridLayouts.EmptyTile)
                {
                    var copyLevel = CopyLevel(level.Grid);
                    copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                    copyLevel[x, y] = GridLayouts.HeroTile;

                    if (!ExcludeSolution(copyLevel, level.StepsCount))
                        _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
                }
                else
                {
                    if (y - 1 >= 0 && level.Grid[x, y - 1] == GridLayouts.EmptyTile)
                    {
                        var copyLevel = CopyLevel(level.Grid);

                        copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                        copyLevel[x, y - 1] = copyLevel[x, y];
                        copyLevel[x, y] = GridLayouts.HeroTile;

                        if (!ExcludeSolution(copyLevel, level.StepsCount, (x, y - 1)))
                            _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
                    }
                }
            }
        }
        else if (key == ConsoleKey.UpArrow)
        {
            var x = heroIndex.x - 1;
            var y = heroIndex.y;
            if (x >= 0 && level.Grid[x, y] != GridLayouts.Wall)
            {
                if (level.Grid[x, y] == GridLayouts.EmptyTile)
                {
                    var copyLevel = CopyLevel(level.Grid);
                    copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                    copyLevel[x, y] = GridLayouts.HeroTile;

                    if (!ExcludeSolution(copyLevel, level.StepsCount))
                        _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
                }
                else
                {
                    if (x - 1 >= 0 && level.Grid[x - 1, y] == GridLayouts.EmptyTile)
                    {
                        var copyLevel = CopyLevel(level.Grid);

                        copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                        copyLevel[x - 1, y] = copyLevel[x, y];
                        copyLevel[x, y] = GridLayouts.HeroTile;


                        if (!ExcludeSolution(copyLevel, level.StepsCount, (x - 1, y)))
                            _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
                    }
                }
            }
        }
        else if (key == ConsoleKey.DownArrow)
        {
            var x = heroIndex.x + 1;
            var y = heroIndex.y;
            if (x < _xLength && level.Grid[x, y] != GridLayouts.Wall)
            {
                if (level.Grid[x, y] == GridLayouts.EmptyTile)
                {
                    var copyLevel = CopyLevel(level.Grid);

                    copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                    copyLevel[x, y] = GridLayouts.HeroTile;

                    if (!ExcludeSolution(copyLevel, level.StepsCount))
                        _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
                }
                else
                {
                    if (x + 1 < _xLength && level.Grid[x + 1, y] == GridLayouts.EmptyTile)
                    {
                        var copyLevel = CopyLevel(level.Grid);
                        copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                        copyLevel[x + 1, y] = copyLevel[x, y];
                        copyLevel[x, y] = GridLayouts.HeroTile;

                        if (!ExcludeSolution(copyLevel, level.StepsCount, (x + 1, y)))
                            _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
                    }
                }
            }
        }
        else if (key == ConsoleKey.RightArrow)
        {
            var x = heroIndex.x;
            var y = heroIndex.y + 1;
            if (y < _yLength && level.Grid[x, y] != GridLayouts.Wall)
            {
                if (level.Grid[x, y] == GridLayouts.EmptyTile)
                {
                    var copyLevel = CopyLevel(level.Grid);
                    copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                    copyLevel[x, y] = GridLayouts.HeroTile;

                    if (!ExcludeSolution(copyLevel, level.StepsCount))
                        _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
                }
                else
                {
                    if (y + 1 < _yLength && level.Grid[x, y + 1] == GridLayouts.EmptyTile)
                    {
                        var copyLevel = CopyLevel(level.Grid);
                        copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                        copyLevel[x, y + 1] = copyLevel[x, y];
                        copyLevel[x, y] = GridLayouts.HeroTile;

                        if (!ExcludeSolution(copyLevel, level.StepsCount, (x, y + 1)))
                            _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
                    }
                }
            }
        }

        if (!_levelsToBruteforce.Any())
        {
            Console.WriteLine("Input not possible. Try again");
            _levelsToBruteforce.Enqueue(level);
            return false;
        }

        return true;

    }

    static void Solve(Level level)
    {
        var heroIndex = level.HeroIndex;

        //try left
        var x = heroIndex.x;
        var y = heroIndex.y - 1;
        if (y >= 0 && level.Grid[x, y] != GridLayouts.Wall)
        {
            if (level.Grid[x, y] == GridLayouts.EmptyTile)
            {
                var copyLevel = CopyLevel(level.Grid);
                copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                copyLevel[x, y] = GridLayouts.HeroTile;

                if (!ExcludeSolution(copyLevel, level.StepsCount))
                    _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            }
            else
            {
                if (y - 1 >= 0 && level.Grid[x, y - 1] == GridLayouts.EmptyTile)
                {
                    var copyLevel = CopyLevel(level.Grid);

                    copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                    copyLevel[x, y - 1] = copyLevel[x, y];
                    copyLevel[x, y] = GridLayouts.HeroTile;

                    if (!ExcludeSolution(copyLevel, level.StepsCount, (x, y - 1)))
                        _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
                }
            }
        }

        //try up
        x = heroIndex.x - 1;
        y = heroIndex.y;
        if (x >= 0 && level.Grid[x, y] != GridLayouts.Wall)
        {
            if (level.Grid[x, y] == GridLayouts.EmptyTile)
            {
                var copyLevel = CopyLevel(level.Grid);
                copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                copyLevel[x, y] = GridLayouts.HeroTile;

                if (!ExcludeSolution(copyLevel, level.StepsCount))
                    _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            }
            else
            {
                if (x - 1 >= 0 && level.Grid[x - 1, y] == GridLayouts.EmptyTile)
                {
                    var copyLevel = CopyLevel(level.Grid);

                    copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                    copyLevel[x - 1, y] = copyLevel[x, y];
                    copyLevel[x, y] = GridLayouts.HeroTile;


                    if (!ExcludeSolution(copyLevel, level.StepsCount, (x - 1, y)))
                        _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
                }
            }
        }

        //try right
        x = heroIndex.x;
        y = heroIndex.y + 1;
        if (y < _yLength && level.Grid[x, y] != GridLayouts.Wall)
        {
            if (level.Grid[x, y] == GridLayouts.EmptyTile)
            {
                var copyLevel = CopyLevel(level.Grid);
                copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                copyLevel[x, y] = GridLayouts.HeroTile;

                if (!ExcludeSolution(copyLevel, level.StepsCount))
                    _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            }
            else
            {
                if (y + 1 < _yLength && level.Grid[x, y + 1] == GridLayouts.EmptyTile)
                {
                    var copyLevel = CopyLevel(level.Grid);
                    copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                    copyLevel[x, y + 1] = copyLevel[x, y];
                    copyLevel[x, y] = GridLayouts.HeroTile;

                    if (!ExcludeSolution(copyLevel, level.StepsCount, (x, y + 1)))
                        _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
                }
            }
        }

        // try down
        x = heroIndex.x + 1;
        y = heroIndex.y;
        if (x < _xLength && level.Grid[x, y] != GridLayouts.Wall)
        {
            if (level.Grid[x, y] == GridLayouts.EmptyTile)
            {
                var copyLevel = CopyLevel(level.Grid);

                copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                copyLevel[x, y] = GridLayouts.HeroTile;

                if (!ExcludeSolution(copyLevel, level.StepsCount))
                    _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
            }
            else
            {
                if (x + 1 < _xLength && level.Grid[x + 1, y] == GridLayouts.EmptyTile)
                {
                    var copyLevel = CopyLevel(level.Grid);
                    copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                    copyLevel[x + 1, y] = copyLevel[x, y];
                    copyLevel[x, y] = GridLayouts.HeroTile;

                    if (!ExcludeSolution(copyLevel, level.StepsCount, (x + 1, y)))
                        _levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level));
                }
            }
        }
    }

    static void PrintLevel(byte[,] level)
    {
        Console.WriteLine("Printing Level");

        for (int i = 0; i < _xLength; i++)
        {
            byte[] col = new byte[_yLength];
            for (int j = 0; j < _yLength; j++)
            {
                col[j] = level[i, j];
            }

            Console.WriteLine(string.Join(",", col));
        }

        Console.WriteLine();
        Console.WriteLine("End");
        Console.WriteLine();
    }

    static byte[,] CopyLevel(byte[,] level)
    {
        byte[,] copyArray = new byte[_xLength, _yLength];

        for (int i = 0; i < _xLength; i++)
        {
            for (int j = 0; j < _yLength; j++)
            {
                copyArray[i, j] = level[i, j];
            }
        }

        return copyArray;
    }

    static int excludedSolutions = 0;

    static bool ExcludeSolution(byte[,] level, short stepsCount, (int x, int y)? boxIndices = null)
    {
        var snapshot = Level.GenerateSnapshot(level);
        if (!boxIndices.HasValue)
        {
            if(_visitedLevelsSnapshots.ContainsKey(snapshot) && _visitedLevelsSnapshots[snapshot] < stepsCount)
            {
                excludedSolutions++;
                return true;
            }

            if (_pendingLevelsSnapshots.ContainsKey(snapshot))
            {
                if(_pendingLevelsSnapshots[snapshot] < stepsCount)
                {
                    excludedSolutions++;
                    return true;
                }
                else
                {
                    _pendingLevelsSnapshots[snapshot] = stepsCount;
                    return false;
                }
            }

            return false;
        }
        else
        {
            //check if all boxes are reachable from current solution
            if (WallChecksInvalidateSolution(level, boxIndices.Value))
            {
                return true;
            }
            if (_visitedLevelsSnapshots.ContainsKey(snapshot) && _visitedLevelsSnapshots[snapshot] < stepsCount)
            {
                excludedSolutions++;
                return true;
            }
            if (_pendingLevelsSnapshots.ContainsKey(snapshot) && (_pendingLevelsSnapshots[snapshot] < stepsCount))
            {
                if (_pendingLevelsSnapshots[snapshot] < stepsCount)
                {
                    excludedSolutions++;
                    return true;
                }
                else
                {
                    _pendingLevelsSnapshots[snapshot] = stepsCount;
                    return false;
                }
            }

            return false;
        }
    }

    static int wallChecksFuncInvalidationsHack = 0;
    static int wallChecksFuncInvalidations = 0;
    static int wallChecksFuncInvalidationsQuadro = 0;

    static bool WallChecksInvalidateSolution(byte[,] level, (int x, int y) boxIndices)
    {
        var isBoxSolved = _currentSolutions.ContainsKey((boxIndices.x, boxIndices.y)) && (_currentSolutions[(boxIndices.x, boxIndices.y)] == level[boxIndices.x, boxIndices.y]);
        if (isBoxSolved)
        {
            return false;
        }

        //todo all remaining boxes have a reachable path

        //lvl hack
        if (GridLayouts.Levle57InvalidationImprovement(boxIndices))
        {
            wallChecksFuncInvalidationsHack++;
            return true;
        }

        //lvl 2 hack
        //if (boxIndices.x == 1 || boxIndices.x == 6 || boxIndices.y == 1 || boxIndices.y == 6)
        //{
        //    return true;
        //}
        //if (SokobanJuniorLayouts.Level15InvalidationImprovement(boxIndices))
        //{
        //    wallChecksFuncInvalidationsHack++;
        //    return true;
        //}

        //check is box stuck in a corner
        bool topWall = level[boxIndices.x - 1, boxIndices.y] == GridLayouts.Wall;
        bool leftWall = level[boxIndices.x, boxIndices.y - 1] == GridLayouts.Wall;
        bool rightWall = level[boxIndices.x, boxIndices.y + 1] == GridLayouts.Wall;
        bool bottomWall = level[boxIndices.x + 1, boxIndices.y] == GridLayouts.Wall;
        if ((topWall && leftWall) || (topWall && rightWall) || (bottomWall && leftWall) || (bottomWall && rightWall))
        {
            wallChecksFuncInvalidations++;
            return true;
        }

        //immovable quaTro setup
        if (level[boxIndices.x - 1, boxIndices.y] != GridLayouts.EmptyTile && level[boxIndices.x - 1, boxIndices.y] != GridLayouts.HeroTile) //top immovable
        {
            if (level[boxIndices.x - 1, boxIndices.y - 1] != GridLayouts.EmptyTile && level[boxIndices.x - 1, boxIndices.y - 1] != GridLayouts.HeroTile // top left
                && level[boxIndices.x, boxIndices.y - 1] != GridLayouts.EmptyTile && level[boxIndices.x, boxIndices.y - 1] != GridLayouts.HeroTile) // left
            {
                wallChecksFuncInvalidationsQuadro++;
                return true;

            }
            else if (level[boxIndices.x - 1, boxIndices.y + 1] != GridLayouts.EmptyTile && level[boxIndices.x - 1, boxIndices.y + 1] != GridLayouts.HeroTile
                && level[boxIndices.x, boxIndices.y + 1] != GridLayouts.EmptyTile && level[boxIndices.x, boxIndices.y + 1] != GridLayouts.HeroTile) // top right + right
            {
                wallChecksFuncInvalidationsQuadro++;
                return true;
            }
        }
        if (level[boxIndices.x + 1, boxIndices.y] != GridLayouts.EmptyTile && level[boxIndices.x + 1, boxIndices.y] != GridLayouts.HeroTile) // bottom immovable
        {
            if (level[boxIndices.x + 1, boxIndices.y - 1] != GridLayouts.EmptyTile && level[boxIndices.x + 1, boxIndices.y - 1] != GridLayouts.HeroTile // bottom left
                && level[boxIndices.x, boxIndices.y - 1] != GridLayouts.EmptyTile && level[boxIndices.x, boxIndices.y - 1] != GridLayouts.HeroTile) // left
            {
                wallChecksFuncInvalidationsQuadro++;
                return true;
            }
            else if (level[boxIndices.x + 1, boxIndices.y + 1] != GridLayouts.EmptyTile && level[boxIndices.x + 1, boxIndices.y + 1] != GridLayouts.HeroTile // bottom right 
                && level[boxIndices.x, boxIndices.y + 1] != GridLayouts.EmptyTile && level[boxIndices.x, boxIndices.y + 1] != GridLayouts.HeroTile) // right
            {
                wallChecksFuncInvalidationsQuadro++;
                return true;
            }
        }

        return false;
    }
}