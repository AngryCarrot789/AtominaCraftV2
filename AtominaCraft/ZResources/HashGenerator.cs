using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.ZResources
{
    public static class HashGenerator
    {
        public static int GenerateHashXZ(int x, int z)
        {
            return x & int.MaxValue | (z & int.MaxValue) << 31;
        }
    }
}
