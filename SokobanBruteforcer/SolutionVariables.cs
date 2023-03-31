namespace SokobanBruteforcer
{
    public static class SolutionVariables
    {
        public static int _xLength;
        public static int _yLength;
        public static IndexedQueue<Level> _levelsToBruteforce;
        public static int _bestSteps = int.MaxValue;
        public static Level _bestLevel;

        public static Dictionary<byte[,], short> _visitedLevelsSnapshotsByte;
        public static Dictionary<byte[,], short> _pendingLevelsSnapshotsByte;

        public static Dictionary<string, short> _visitedLevelsSnapshots;
        public static Dictionary<string, short> _pendingLevelsSnapshots;
        public static Dictionary<(int x, int y), byte> _currentSolutions;
        public static bool foundSolution = false;


        //improvements
        public static bool useStringSnapshotting = true;
        public static Predicate<(int x, int y)> _levelInvalidationImprovement = (_) => false;

        //testing
        public static byte highestSolvedItemsCount = 0;
        public static string solvedItemsSnapshot = "";
    }
}
