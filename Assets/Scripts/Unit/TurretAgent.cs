using UnityEngine;
using Random = UnityEngine.Random;

public class TurretAgent : MonoBehaviour
{
    [SerializeField] private BulletAgent _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletLife;
    [SerializeField] private float _rechargeTime;

    
    private float _alpha;
    private float _countdown;

    void Awake()
    {
        _alpha = (int) (Random.value * 4) * 90;
        _countdown = _rechargeTime;
        transform.eulerAngles = new Vector3(0, _alpha, 0);
    }
    
    void Update()
    {
        _countdown -= Time.deltaTime;
        if (_countdown <= 0)
        {
            _countdown = _rechargeTime;
            var direction = Quaternion.AngleAxis(_alpha, Vector3.up) * Vector3.forward;
            var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            bullet.Constructor( _bulletSpeed * direction, _bulletLife);
        }
    }
}
