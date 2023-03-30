﻿using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SokobanBruteforcer
{
    public class ByteArrayComparer : IComparable<byte[,]>, IEqualityComparer<byte[,]>
    {
        public int CompareTo(byte[,]? other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(byte[,]? x, byte[,]? y)
        {
            for (int i = 0; i < Program._xLength; i++)
            {
                for (int j = 0; j < Program._yLength; j++)
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
            StringBuilder sb = new StringBuilder(Program._xLength * Program._yLength);
            for (int i = 0; i < Program._xLength; i++)
            {
                for (int j = 0; j < Program._yLength; j++)
                {
                    sb.Append(obj[i, j].ToString());
                }
            }

            return sb.ToString().GetHashCode();
        }
    }

}