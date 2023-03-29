namespace SokobanBruteforcer
{
    public static class GridUtils
    {
        public static int _xLength = Program._xLength;
        public static int _yLength = Program._yLength;

        public static byte[,] CopyLevel(byte[,] level)
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
    }
}
