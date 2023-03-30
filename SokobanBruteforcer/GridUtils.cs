namespace SokobanBruteforcer
{
    public static class GridUtils
    {

        public static byte[,] CopyLevel(byte[,] level)
        {
            byte[,] copyArray = new byte[SolutionVariables._xLength, SolutionVariables._yLength];

            for (int i = 0; i < SolutionVariables._xLength; i++)
            {
                for (int j = 0; j < SolutionVariables._yLength; j++)
                {
                    copyArray[i, j] = level[i, j];
                }
            }

            return copyArray;
        }
    }
}
