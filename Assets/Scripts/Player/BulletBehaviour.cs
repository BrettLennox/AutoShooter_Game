using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject _target;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    Vector3 dir;
    #endregion
    #region Properties
    public GameObject Target { get => _target; set => _target = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public int Damage { get => _damage; set => _damage = value; }
    #endregion
    #region References
    private EnemyTracker _enemyTracker;
    #endregion

    private void Awake()
    {
        _enemyTracker = GameObject.FindWithTag("Player").GetComponent<EnemyTracker>();
    }
    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
        if(Target == null || !Target.activeInHierarchy)
        {
            Target = _enemyTracker.ClosestEnemy(transform);
        }


        //if (Target != null) { MoveTowardsTarget(); }
        //else
        //{
        //    if (_enemyTracker.ClosestEnemy() != null)
        //    {
        //        Target = _enemyTracker.ClosestEnemy();
        //    }
        //    else
        //    {
        //        GameObject particleSystem = ObjectPool.SharedInstance.GetPooledObject("BulletImpact_ParticleSystem");
        //        if (particleSystem != null)
        //        {
        //            particleSystem.transform.position = transform.position;
        //            particleSystem.transform.rotation = Quaternion.identity;
        //            particleSystem.SetActive(true);
        //        }
        //        this.gameObject.SetActive(false);
        //    }
        //}
        
    }

    private void MoveTowardsTarget()
    {
        dir = Target.transform.position - transform.position;
        dir.Normalize();

        transform.Translate((dir * Speed) * Time.deltaTime);
    }
}
