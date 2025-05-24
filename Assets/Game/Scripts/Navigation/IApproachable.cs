using System.Collections.Generic;
using UnityEngine;

namespace Game.Navigation
{
    public interface IApproachable
    {
        public List<Vector3> GetPossibleDestinationPoints();
    }
}