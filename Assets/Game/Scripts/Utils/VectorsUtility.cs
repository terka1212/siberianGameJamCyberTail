using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils
{
    public class VectorsUtility
    {
        public static Vector3 FindNearestVector3(Vector3 origin, List<Vector3> vectors)
        {
            var result = vectors[0];
            for (int i = 1; i < vectors.Count; i++)
            {
                if (Vector3.Distance(result, origin) > Vector3.Distance(vectors[i], origin))
                {
                    result = vectors[i];
                }
            }
            return result;
        }
    }
}