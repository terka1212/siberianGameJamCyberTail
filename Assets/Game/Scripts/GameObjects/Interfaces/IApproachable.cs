using System.Collections.Generic;
using UnityEngine;

namespace Game.GameObjects
{
    public interface IApproachable
    {
        public List<Vector3> GetPossibleDestinationPoints();
    }
}