using System;
using UnityEngine;

namespace Turret
{
    public class TurretView : MonoBehaviour
    {
        [SerializeField] private Transform _projectileOrigin;
        [SerializeField] private Transform _tower;

        private TurretData _data;
        public TurretData Data => _data;

        public Transform Tower => _tower;
        public Transform ProjectileOrigin => _projectileOrigin;

        public void AttachData(TurretData data)
        {
            _data = data;
        }
    }
}