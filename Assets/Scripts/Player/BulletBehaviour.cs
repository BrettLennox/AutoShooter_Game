using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject _target;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    private float _timer = 0f;
    private float _maxPlayerTargetTimer = 5f;
    private bool _isNewChasePeriod = false;
    Vector3 dir;
    #endregion
    #region Properties
    public GameObject Target { get => _target; set => _target = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public int Damage { get => _damage; set => _damage = value; }
    public GameObject Player { get => _player; set => _player = value; }
    public EnemyTracker EnemyTracker { get => _enemyTracker; set => _enemyTracker = value; }
    #endregion
    #region References
    private EnemyTracker _enemyTracker;
    private GameObject _player;
    #endregion

    private void SetReferences()
    {
        Player = GameObject.FindWithTag("Player");
        EnemyTracker = Player.GetComponent<EnemyTracker>();
    }

    private void OnValidate()
    {
        SetReferences();
    }

    private void Awake()
    {
        SetReferences();
    }
    // Update is called once per frame
    void Update()
    {
        if (Target != null) //if Target != null => Performs MoveTowardsTarget function
            MoveTowardsTarget();
        if (Target == null || !Target.activeInHierarchy) //if Target == null and Target is not active in heirarchy => performs ChangeTarget function with EnemyTracker.ClosestEnemy function return
        {
            ChangeTarget(EnemyTracker.ClosestEnemy(transform));
        }
        if (Target == null && EnemyTracker.ClosestEnemy(transform) == null) //if Target == null and EnemyTracker.ClosestEnemy returns null => Perform ChangeTarget function with target set to Player
        {
            ChangeTarget(Player);
        }
        if (Target == Player) //if Target == Player 
        {
            if (EnemyTracker.ClosestEnemy(transform) == null) //if EnemyTracker.ClosestEnemy returns null
            {
                _isNewChasePeriod = true; //sets isNewChasePeriod to true

                if (_isNewChasePeriod) //if isNewChasePeriod is true => increments timer by Time.deltaTime
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _maxPlayerTargetTimer) //if timer >= maxPlayerTargetTimer => enables a BulletImpact_ParticleSystem at the position of the bullet, and then disables bullet gameObject
                    {
                        GameObject particleSystem = ObjectPool.SharedInstance.GetPooledObject("BulletImpact_ParticleSystem");
                        if (particleSystem != null)
                        {
                            particleSystem.transform.position = transform.position;
                            particleSystem.transform.rotation = Quaternion.identity;
                            particleSystem.SetActive(true);
                        }
                        this.gameObject.SetActive(false);
                    }
                }
            }
            else //if EnemyTracker.ClosestEnemy returns a gameObject => Performs ChangeTarget function with ClosestEnemy, sets timer back to 0f, sets isNewChasePeriod back to false
            {
                ChangeTarget(EnemyTracker.ClosestEnemy(transform));
                _timer = 0f;
                _isNewChasePeriod = false;
            }
        }

    }

    private void ChangeTarget(GameObject target) //Changes Target to the passed in GameObject
    {
        Target = target;
    }

    private void MoveTowardsTarget() // calculates a Vector direction from this transform to the target transform and then moves the transform towards that Vector multiplied by Speed and Time.deltaTime
    {
        dir = Target.transform.position - transform.position;
        dir = dir.normalized;

        transform.Translate((dir * Speed) * Time.deltaTime);
    }
}
