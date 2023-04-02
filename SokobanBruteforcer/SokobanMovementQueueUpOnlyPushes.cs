namespace SokobanBruteforcer
{
    public static class SokobanMovementQueueUpOnlyPushes
    {
        private static HashSet<byte> allBoxTiles = new() { 3, 4, 5, 6, 7, 9 };
        private static Queue<(byte[,] grid, short steps, Direction incomingDir)> gridsToTraverse = new Queue<(byte[,], short, Direction)>(10);
        private static Dictionary<byte[,], short> visitedGridsWithSteps = new Dictionary<byte[,], short>(10, new ByteArrayComparer());

        public static int SolveWhileQueueingOnlyMovementsCount = 0;
        public static int SolveWhileQueueingOnlyMovementsDequeueCount = 0;
        public static int SolveWhileQueueingOnlyMovementsProcessedCount = 0;

        public static void SolveWhileQueueingOnlyMovements(Level level, bool skipExlusionChecks = false)
        {
            SolveWhileQueueingOnlyMovementsCount += 1;
            gridsToTraverse.Enqueue((level.Grid, level.StepsCount, level.Pushed ? Direction.None : level.IncomingDirection));

            while (gridsToTraverse.Any())
            {
                var currentGrid = gridsToTraverse.Dequeue();
                SolveWhileQueueingOnlyMovementsDequeueCount += 1;
                if (SolutionVariables._bestSteps <= currentGrid.steps)
                {
                    continue;
                }

                if (!visitedGridsWithSteps.ContainsKey(currentGrid.grid))
                {
                    visitedGridsWithSteps.Add(currentGrid.grid, currentGrid.steps);
                }
                else
                {
                    if(visitedGridsWithSteps[currentGrid.grid] <= currentGrid.steps)
                    {
                        continue;
                    }
                    else
                    {
                        visitedGridsWithSteps[currentGrid.grid] = currentGrid.steps;
                    }
                }

                SolveWhileQueueingOnlyMovementsProcessedCount += 1;

                short shortCurrentStepsInc = (short)(currentGrid.steps + 1);
                var heroIndex = GridUtils.FindHeroIndex(currentGrid.grid);

                var x = heroIndex.x;
                var y = heroIndex.y - 1;
                //check if left is a box
                //else try left if empty
                if(y >= 0 && currentGrid.incomingDir != Direction.Right)
                {
                    if (IsBlockTile(currentGrid.grid[x, y]))
                    {
                        var checkpointLevel = new Level(currentGrid.grid, previousLevel: level, currentGrid.steps);
                        SokobanMovement.TryMoveLeft(checkpointLevel, heroIndex, skipExlusionChecks);
                    }
                    else if(level.Grid[x, y] == GridLayouts.EmptyTile)
                    {
                        var copyLevel = GridUtils.CopyLevel(currentGrid.grid);
                        copyLevel[x, y] = GridLayouts.HeroTile;
                        copyLevel[heroIndex.x, heroIndex.y] = 1;
                        gridsToTraverse.Enqueue((copyLevel, shortCurrentStepsInc, Direction.Left));
                    }
                }

                x = heroIndex.x - 1;
                y = heroIndex.y;
                //check if up is a box
                //else try up if empty
                if(x >= 0 && currentGrid.incomingDir != Direction.Down)
                {
                    if (IsBlockTile(currentGrid.grid[x, y]))
                    {
                        var checkpointLevel = new Level(currentGrid.grid, previousLevel: level, currentGrid.steps);
                        SokobanMovement.TryMoveUp(checkpointLevel, heroIndex, skipExlusionChecks);
                    }
                    else if (level.Grid[x, y] == GridLayouts.EmptyTile)
                    {
                        var copyLevel = GridUtils.CopyLevel(currentGrid.grid);
                        copyLevel[x, y] = GridLayouts.HeroTile;
                        copyLevel[heroIndex.x, heroIndex.y] = 1;
                        gridsToTraverse.Enqueue((copyLevel, shortCurrentStepsInc, Direction.Up));
                    }
                }

                x = heroIndex.x;
                y = heroIndex.y + 1;
                //check if right is a box
                //else try right if empty
                if(y < SolutionVariables._yLength && currentGrid.incomingDir != Direction.Left)
                {
                    if (IsBlockTile(currentGrid.grid[x, y]))
                    {
                        var checkpointLvl = new Level(currentGrid.grid, level, currentGrid.steps);
                        SokobanMovement.TryMoveRight(checkpointLvl, heroIndex, skipExlusionChecks);
                    }
                    else if (level.Grid[x, y] == GridLayouts.EmptyTile)
                    {
                        var copyLevel = GridUtils.CopyLevel(currentGrid.grid);
                        copyLevel[x, y] = GridLayouts.HeroTile;
                        copyLevel[heroIndex.x, heroIndex.y] = 1;
                        gridsToTraverse.Enqueue((copyLevel, shortCurrentStepsInc, Direction.Right));
                    }
                }

                x = heroIndex.x + 1;
                y = heroIndex.y;
                //check if down is abox
                //else try down if empty
                if (x < SolutionVariables._xLength && currentGrid.incomingDir != Direction.Up)
                {
                    if (IsBlockTile(currentGrid.grid[x, y]))
                    {
                        var checkpointLvl = new Level(currentGrid.grid, level, currentGrid.steps);
                        SokobanMovement.TryMoveDown(checkpointLvl, heroIndex, skipExlusionChecks);
                    }
                    else if (level.Grid[x, y] == GridLayouts.EmptyTile)
                    {
                        var copyLevel = GridUtils.CopyLevel(currentGrid.grid);
                        copyLevel[x, y] = GridLayouts.HeroTile;
                        copyLevel[heroIndex.x, heroIndex.y] = 1;
                        gridsToTraverse.Enqueue((copyLevel, shortCurrentStepsInc, Direction.Down));
                    }
                }
            }

            gridsToTraverse.Clear();//should be cleared anyway
            visitedGridsWithSteps.Clear();
        }

        private static short AddSteps(short levelStepsCount, short additionalSteps)
        {
            return (short)(levelStepsCount + additionalSteps);
        }

        public static bool IsBlockTile(byte tile)
        {
            return allBoxTiles.Contains(tile);
        }
    }
}
