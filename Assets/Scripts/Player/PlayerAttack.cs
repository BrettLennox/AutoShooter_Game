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

    // Update is called once per frame
    void Update()
    {
        _bulletTimer += Time.deltaTime;

        if (_bulletTimer >= _equippedWeapon.weaponData.fireRate && GetComponent<EnemyTracker>().ClosestEnemy() != null)
        {
            _bulletTimer = 0;
            GameObject closestTarget = GetComponent<EnemyTracker>().ClosestEnemy();
            GameObject bullet = ObjectPool.SharedInstance.GetPooledObject("Bullet");
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;

                BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
                bulletBehaviour.Target = GetComponent<EnemyTracker>().ClosestEnemy();
                bulletBehaviour.Damage = _equippedWeapon.weaponData.weaponDamage;
                bullet.SetActive(true);
            }
        }
    }
}
