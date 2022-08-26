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
    MousePosition _mousePosition;
    #endregion

    private void Awake()
    {
        Setup();
    }

    private void OnValidate()
    {
        Setup();
    }

    private void Setup()
    {
        if (_enemyTracker == null)
            _enemyTracker = GetComponent<EnemyTracker>();
        if (_mousePosition == null)
            _mousePosition = GetComponent<MousePosition>();
    }

    // Update is called once per frame
    void Update()
    {
        _bulletTimer += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            Attack(_mousePosition.CurrentMousePosition());
        }
    }

    private void Attack(Vector3 targetPos)
    {
        if (_bulletTimer >= _equippedWeapon.weaponData.fireRate)
        {
            _bulletTimer = 0;
            GameObject projectile = ObjectPool.SharedInstance.GetPooledObject("Bullet");
            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.identity;

                BulletBehaviour bulletBehaviour = projectile.GetComponent<BulletBehaviour>();
                bulletBehaviour.Target = targetPos;
                bulletBehaviour.Damage = _equippedWeapon.weaponData.weaponDamage;
                bulletBehaviour.Speed = _equippedWeapon.weaponData.bulletSpeed;
                projectile.SetActive(true);
            }
        }
    }
}
