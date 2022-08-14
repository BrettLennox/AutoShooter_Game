using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletTimer = Mathf.Infinity;
    [SerializeField] private float _timeBetweenShots = 2f;
    
    private GameObject _closestTarget;
    #endregion
    #region Properties
    public GameObject BulletPrefab { get => _bulletPrefab; private set => _bulletPrefab = value; }
    public float TimeBetweenShots { get => _timeBetweenShots; set => _timeBetweenShots = value; }
    #endregion

    // Update is called once per frame
    void Update()
    {
        _bulletTimer += Time.deltaTime;

        if(_bulletTimer >= TimeBetweenShots)
        {
            _bulletTimer = 0;
            _closestTarget = GetComponent<EnemyTracker>().ClosestEnemy();
            var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<BulletBehaviour>();
            bullet.Target = _closestTarget;
        }
    }
}
