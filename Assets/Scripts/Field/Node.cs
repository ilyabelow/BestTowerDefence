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

        private GameObject _occupant;
        public bool Occupied => _occupant != null;
        
        public Node NextNode  { get; set; }
        public float PathWeight  { get; set; }
        public bool Visited { get; set; }
        public OccupationAvailability Availability { get; set; }
        
        public void Occupy(GameObject occupant)
        {
            if (Occupied)
            {
                Release();
            }
            _occupant = occupant;
        }

        public void Release()
        {
            Object.Destroy(_occupant);
            _occupant = null;
        }
        
        public void Reset()
        {
            PathWeight = float.MaxValue;
            Availability = OccupationAvailability.CanOccupy;
        }
        
    }
}