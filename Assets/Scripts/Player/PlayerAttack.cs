using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Variables
    [SerializeField] private WeaponData _equippedWeapon;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletTimer = Mathf.Infinity;
    #endregion
    #region Properties
    public GameObject BulletPrefab { get => _bulletPrefab; private set => _bulletPrefab = value; }
    #endregion
    #region References
    EnemyTracker _enemyTracker;
    #endregion

    private void Awake()
    {
        _enemyTracker = GetComponent<EnemyTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        _bulletTimer += Time.deltaTime;

        if (_bulletTimer >= _equippedWeapon.weaponData.fireRate && _enemyTracker.ClosestEnemy(transform) != null)
        {
            _bulletTimer = 0;
            GameObject closestTarget = _enemyTracker.ClosestEnemy(transform);
            GameObject bullet = ObjectPool.SharedInstance.GetPooledObject("Bullet");
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;

                BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
                bulletBehaviour.Target = _enemyTracker.ClosestEnemy(transform);
                bulletBehaviour.Damage = _equippedWeapon.weaponData.weaponDamage;
                bullet.SetActive(true);
            }
        }
    }
}
