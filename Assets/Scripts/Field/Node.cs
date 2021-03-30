using Turret;
using UnityEngine;

namespace Field
{
    public enum OccupationAvailability
    {
        CanOccupy,
        CanNotOccupy,
        Undefined
    }

    public class Node
    {
        public Vector3 Position { get; set; }

        private TurretData _occupant;
        public bool Occupied => _occupant != null;

        public Node NextNode { get; set; }
        public float PathWeight { get; set; }
        public bool Visited { get; set; }
        public OccupationAvailability Availability { get; set; }

        public void Occupy(TurretData occupant)
        {
            _occupant = occupant;
        }

        public TurretData Release()
        {
            var ret = _occupant;
            _occupant = null;
            return ret;
        }

        public void Reset()
        {
            PathWeight = float.MaxValue;
            Availability = OccupationAvailability.CanOccupy;
        }
    }
}