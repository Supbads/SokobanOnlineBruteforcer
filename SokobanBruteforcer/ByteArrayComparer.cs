using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SokobanBruteforcer
{
    public class ByteArrayComparer : IEqualityComparer<byte[,]>
    {
        public static int EqualsChecks = 0;
        public static int HashCodes = 0;
        private static StringBuilder sb;

        public ByteArrayComparer()
        {
            sb = new StringBuilder(SolutionVariables._xLength * SolutionVariables._yLength);
        }

        public bool Equals(byte[,]? x, byte[,]? y)
        {
            EqualsChecks++;
            for (int i = 0; i < SolutionVariables._xLength; i++)
            {
                for (int j = 0; j < SolutionVariables._yLength; j++)
                {
                    if (x[i, j] != y[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //old
        public int GetHashCode([DisallowNull] byte[,] obj)
        {
            HashCodes++;
            sb.Clear();
            for (int i = 0; i < SolutionVariables._xLength; i++)
            {
                for (int j = 0; j < SolutionVariables._yLength; j++)
                {
                    sb.Append(obj[i, j].ToString());
                }
            }

            return sb.ToString().GetHashCode();
        }

        public int GetHashCodeV2([DisallowNull] byte[,] grid)
        {
            unchecked
            {
                int hash = 17;

                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j] != 0)
                        {
                            hash = hash * 23 + Convert.ToInt32(Math.Pow(grid[i, j], i));
                            hash = hash * 23 + Convert.ToInt32(Math.Pow(grid[i, j], j));
                        }
                    }
                }

                return hash;
            }
        }
    }

}
