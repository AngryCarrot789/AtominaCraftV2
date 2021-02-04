using AtominaCraft.ZResources.Maths;
using System.Buffers;
using System.Collections.Generic;
using System.Security.Principal;

namespace AtominaCraft.Memory.Pools
{
    /// <summary>
    /// Not sure if this should be used yet...
    /// </summary>
    public static class VectorPool
    {
        private static Dictionary<float, Vector3> Pool { get; set; }

        public static void Initialise(int poolCapacity)
        {
            Pool = new Dictionary<float, Vector3>(poolCapacity);
        }

        public static Vector3 GetVector(float x, float y, float z)
        {
            float total = x + y + z;
            return Pool.TryGetValue(total, out Vector3 vector) ? vector : new Vector3(x, y, z);
        }
    }
}
