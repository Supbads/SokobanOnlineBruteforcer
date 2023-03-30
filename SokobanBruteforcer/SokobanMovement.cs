namespace SokobanBruteforcer
{
    public static class SokobanMovement
    {
        public static int _xLength = Program._xLength;
        public static int _yLength = Program._yLength;

        public static void TryMoveRight(Level level, bool skipExlusionChecks = true)
        {
            var heroIndex = level.HeroIndex;
            var x = heroIndex.x;
            var y = heroIndex.y + 1;
            if (y < _yLength && level.Grid[x, y] != GridLayouts.Wall && level.Grid[x, y] != GridLayouts.Hole)
            {
                if (level.Grid[x, y] == GridLayouts.EmptyTile)
                {
                    var copyLevel = GridUtils.CopyLevel(level.Grid);
                    copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                    copyLevel[x, y] = GridLayouts.HeroTile;

                    if (skipExlusionChecks || !ExcludeSolution(copyLevel, level.StepsCount))
                        Program._levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level, false, Direction.Right));
                }
                else
                {
                    if (y + 1 >= 0 && level.Grid[x, y] == GridLayouts.HoleBlock && level.Grid[x, y + 1] == GridLayouts.Hole)
                    {
                        var copyLevel = GridUtils.CopyLevel(level.Grid);

                        copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                        copyLevel[x, y + 1] = GridLayouts.EmptyTile;
                        copyLevel[x, y] = GridLayouts.HeroTile;

                        if (skipExlusionChecks || !ExcludeSolution(copyLevel, level.StepsCount, (x, y + 1), true))
                            Program._levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level, true, Direction.Right));
                    }
                    else if (y + 1 < _yLength && level.Grid[x, y + 1] == GridLayouts.EmptyTile)
                    {
                        var copyLevel = GridUtils.CopyLevel(level.Grid);
                        copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                        copyLevel[x, y + 1] = copyLevel[x, y];
                        copyLevel[x, y] = GridLayouts.HeroTile;

                        bool skipWallChecks = copyLevel[x, y - 1] == GridLayouts.HoleBlock;

                        if (skipExlusionChecks || !ExcludeSolution(copyLevel, level.StepsCount, (x, y + 1), skipWallChecks))
                            Program._levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level, true, Direction.Right));
                    }
                }
            }
        }

        public static void TryMoveDown(Level level, bool skipExlusionChecks = false)
        {
            var heroIndex = level.HeroIndex;
            var x = heroIndex.x + 1;
            var y = heroIndex.y;
            if (x < _xLength && level.Grid[x, y] != GridLayouts.Wall && level.Grid[x, y] != GridLayouts.Hole)
            {
                if (level.Grid[x, y] == GridLayouts.EmptyTile)
                {
                    var copyLevel = GridUtils.CopyLevel(level.Grid);

                    copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                    copyLevel[x, y] = GridLayouts.HeroTile;

                    if (skipExlusionChecks || !ExcludeSolution(copyLevel, level.StepsCount))
                        Program._levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level, false, Direction.Down));
                }
                else
                {
                    if (x + 1 >= 0 && level.Grid[x, y] == GridLayouts.HoleBlock && level.Grid[x + 1, y] == GridLayouts.Hole)
                    {
                        var copyLevel = GridUtils.CopyLevel(level.Grid);

                        copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                        copyLevel[x + 1, y] = GridLayouts.EmptyTile;
                        copyLevel[x, y] = GridLayouts.HeroTile;

                        if (skipExlusionChecks || !ExcludeSolution(copyLevel, level.StepsCount, (x + 1, y), true))
                            Program._levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level, true, Direction.Down));
                    }
                    else if (x + 1 < _xLength && level.Grid[x + 1, y] == GridLayouts.EmptyTile)
                    {
                        var copyLevel = GridUtils.CopyLevel(level.Grid);
                        copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                        copyLevel[x + 1, y] = copyLevel[x, y];
                        copyLevel[x, y] = GridLayouts.HeroTile;

                        bool skipWallChecks = copyLevel[x, y - 1] == GridLayouts.HoleBlock;

                        if (skipExlusionChecks || !ExcludeSolution(copyLevel, level.StepsCount, (x + 1, y), skipWallChecks))
                            Program._levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level, true, Direction.Down));
                    }
                }
            }
        }

        public static void TryMoveUp(Level level, bool skipExlusionChecks = true)
        {
            var heroIndex = level.HeroIndex;
            var x = heroIndex.x - 1;
            var y = heroIndex.y;
            if (x >= 0 && level.Grid[x, y] != GridLayouts.Wall && level.Grid[x, y] != GridLayouts.Hole)
            {
                if (level.Grid[x, y] == GridLayouts.EmptyTile)
                {
                    var copyLevel = GridUtils.CopyLevel(level.Grid);
                    copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                    copyLevel[x, y] = GridLayouts.HeroTile;

                    if (skipExlusionChecks || !ExcludeSolution(copyLevel, level.StepsCount))
                        Program._levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level, false, Direction.Up));
                }
                else
                {
                    if (x - 1 >= 0 && level.Grid[x, y] == GridLayouts.HoleBlock && level.Grid[x - 1, y] == GridLayouts.Hole)
                    {
                        var copyLevel = GridUtils.CopyLevel(level.Grid);

                        copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                        copyLevel[x - 1, y] = GridLayouts.EmptyTile;
                        copyLevel[x, y] = GridLayouts.HeroTile;

                        if (skipExlusionChecks || !ExcludeSolution(copyLevel, level.StepsCount, (x - x, y), true))
                            Program._levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level, true, Direction.Up));
                    }
                    else if (x - 1 >= 0 && level.Grid[x - 1, y] == GridLayouts.EmptyTile)
                    {
                        var copyLevel = GridUtils.CopyLevel(level.Grid);

                        copyLevel[heroIndex.x, y] = GridLayouts.EmptyTile;
                        copyLevel[x - 1, y] = copyLevel[x, y];
                        copyLevel[x, y] = GridLayouts.HeroTile;

                        bool skipWallChecks = copyLevel[x, y - 1] == GridLayouts.HoleBlock;

                        if (skipExlusionChecks || !ExcludeSolution(copyLevel, level.StepsCount, (x - 1, y), skipWallChecks))
                            Program._levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level, true, Direction.Up));
                    }
                }
            }
        }

        public static void TryMoveLeft(Level level, bool skipExlusionChecks = true)
        {
            var heroIndex = level.HeroIndex;
            var x = heroIndex.x;
            var y = heroIndex.y - 1;
            if (y >= 0 && level.Grid[x, y] != GridLayouts.Wall && level.Grid[x, y] != GridLayouts.Hole)
            {
                if (level.Grid[x, y] == GridLayouts.EmptyTile)
                {
                    var copyLevel = GridUtils.CopyLevel(level.Grid);
                    copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                    copyLevel[x, y] = GridLayouts.HeroTile;

                    if (skipExlusionChecks || !ExcludeSolution(copyLevel, level.StepsCount))
                        Program._levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level, false, Direction.Left));
                }
                else
                {
                    if (y - 1 >= 0 && level.Grid[x, y] == GridLayouts.HoleBlock && level.Grid[x, y - 1] == GridLayouts.Hole)
                    {
                        var copyLevel = GridUtils.CopyLevel(level.Grid);

                        copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                        copyLevel[x, y - 1] = GridLayouts.EmptyTile;
                        copyLevel[x, y] = GridLayouts.HeroTile;

                        if (skipExlusionChecks || !ExcludeSolution(copyLevel, level.StepsCount, (x, y - 1), true))
                            Program._levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level, true, Direction.Left));
                    }
                    else if (y - 1 >= 0 && level.Grid[x, y - 1] == GridLayouts.EmptyTile)
                    {
                        var copyLevel = GridUtils.CopyLevel(level.Grid);

                        copyLevel[x, heroIndex.y] = GridLayouts.EmptyTile;
                        copyLevel[x, y - 1] = copyLevel[x, y];
                        copyLevel[x, y] = GridLayouts.HeroTile;

                        bool skipWallChecks = copyLevel[x, y - 1 ] == GridLayouts.HoleBlock;

                        if (skipExlusionChecks || !ExcludeSolution(copyLevel, level.StepsCount, (x, y - 1), skipWallChecks))
                            Program._levelsToBruteforce.Enqueue(new Level(copyLevel, (x, y), level, true, Direction.Left));
                    }
                }
            }
        }


        public static int excludedSolutions = 0;

        public static bool ExcludeSolution(byte[,] level, short stepsCount, (int x, int y)? boxIndices = null, bool skipWallChecks = false)
        {
            var snapshot = Level.GenerateSnapshot(level);
            if (!boxIndices.HasValue)
            {
                if (Program._visitedLevelsSnapshots.ContainsKey(snapshot) && Program._visitedLevelsSnapshots[snapshot] < stepsCount)
                {
                    excludedSolutions++;
                    return true;
                }

                if (Program._pendingLevelsSnapshots.ContainsKey(snapshot))
                {
                    if (Program._pendingLevelsSnapshots[snapshot] < stepsCount)
                    {
                        excludedSolutions++;
                        return true;
                    }
                    else
                    {
                        Program._pendingLevelsSnapshots[snapshot] = stepsCount;
                        return false;
                    }
                }

                return false;
            }
            else
            {
                //check if all boxes are reachable from current solution
                if (!skipWallChecks && WallChecksInvalidateSolution(level, boxIndices.Value))
                {
                    return true;
                }
                if (Program._visitedLevelsSnapshots.ContainsKey(snapshot) && Program._visitedLevelsSnapshots[snapshot] < stepsCount)
                {
                    excludedSolutions++;
                    return true;
                }
                if (Program._pendingLevelsSnapshots.ContainsKey(snapshot) && (Program._pendingLevelsSnapshots[snapshot] < stepsCount))
                {
                    if (Program._pendingLevelsSnapshots[snapshot] < stepsCount)
                    {
                        excludedSolutions++;
                        return true;
                    }
                    else
                    {
                        Program._pendingLevelsSnapshots[snapshot] = stepsCount;
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool ExcludeSolutionByteArrayImpl(byte[,] level, short stepsCount, (int x, int y)? boxIndices = null, bool skipWallCheecks = false)
        {
            if (!boxIndices.HasValue)
            {
                if (Program._visitedLevelsSnapshotsByte.ContainsKey(level) && Program._visitedLevelsSnapshotsByte[level] < stepsCount)
                {
                    excludedSolutions++;
                    return true;
                }

                if (Program._pendingLevelsSnapshotsByte.ContainsKey(level))
                {
                    if (Program._pendingLevelsSnapshotsByte[level] < stepsCount)
                    {
                        excludedSolutions++;
                        return true;
                    }
                    else
                    {
                        Program._pendingLevelsSnapshotsByte[level] = stepsCount;
                        return false;
                    }
                }

                return false;
            }
            else
            {
                //check if all boxes are reachable from current solution
                if (!skipWallCheecks && WallChecksInvalidateSolution(level, boxIndices.Value))
                {
                    return true;
                }
                if (Program._visitedLevelsSnapshotsByte.ContainsKey(level) && Program._visitedLevelsSnapshotsByte[level] < stepsCount)
                {
                    excludedSolutions++;
                    return true;
                }
                if (Program._pendingLevelsSnapshotsByte.ContainsKey(level) && (Program._pendingLevelsSnapshotsByte[level] < stepsCount))
                {
                    if (Program._pendingLevelsSnapshotsByte[level] < stepsCount)
                    {
                        excludedSolutions++;
                        return true;
                    }
                    else
                    {
                        Program._pendingLevelsSnapshotsByte[level] = stepsCount;
                        return false;
                    }
                }

                return false;
            }
        }

        public static int wallChecksFuncInvalidationsHack = 0;
        public static int wallChecksFuncInvalidations = 0;
        public static int wallChecksFuncInvalidationsQuadro = 0;

        public static bool WallChecksInvalidateSolution(byte[,] level, (int x, int y) boxIndices)
        {
            var isBoxSolved = Program._currentSolutions.ContainsKey((boxIndices.x, boxIndices.y)) && (Program._currentSolutions[(boxIndices.x, boxIndices.y)] == level[boxIndices.x, boxIndices.y]);
            if (isBoxSolved)
            {
                return false;
            }

            if (level[boxIndices.x, boxIndices.y] == GridLayouts.HoleBlock)
            {
                return false;
            }

            //todo all remaining boxes have a reachable path

            //lvl hack
            //if (GridLayouts.Levle57InvalidationImprovement(boxIndices))
            //{
            //    wallChecksFuncInvalidationsHack++;
            //    return true;
            //}

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
}
