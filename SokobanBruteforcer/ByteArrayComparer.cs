using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SokobanBruteforcer
{
    public class ByteArrayComparer : IEqualityComparer<byte[,]>
    {
        public static int EqualsChecks = 0;
        public static int HashCodes = 0;

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

        public int GetHashCode([DisallowNull] byte[,] obj)
        {
            HashCodes++;
            StringBuilder sb = new StringBuilder(SolutionVariables._xLength * SolutionVariables._yLength);
            for (int i = 0; i < SolutionVariables._xLength; i++)
            {
                for (int j = 0; j < SolutionVariables._yLength; j++)
                {
                    sb.Append(obj[i, j].ToString());
                }
            }

            return sb.ToString().GetHashCode();
        }
    }

}
