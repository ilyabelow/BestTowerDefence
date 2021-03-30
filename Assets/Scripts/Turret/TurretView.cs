using System;
using UnityEngine;

namespace Turret
{
    public class TurretView : MonoBehaviour
    {
        private TurretData _data;
        public TurretData Data => _data;

        [SerializeField] private Transform _projectileOrigin;
        [SerializeField] private Transform _tower;

        public Transform Tower => _tower;
        public Transform ProjectileOrigin => _projectileOrigin;

        public void AttachData(TurretData data)
        {
            _data = data;
        }
    }
}