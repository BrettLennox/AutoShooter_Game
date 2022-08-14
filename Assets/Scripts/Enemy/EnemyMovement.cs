using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform _targetPos;
    [SerializeField] private int _health;

    Vector3 dir;
    #endregion
    #region Properties
    public float Speed { get => _speed; set => _speed = value; }
    public Transform TargetPos { get => _targetPos; set => _targetPos = value; }
    public int Health { get => _health; private set => _health = value; }
    #endregion
    #region References
    private EnemyTracker _enemyTracker;
    #endregion

    private void Awake()
    {
        TargetPos = GameObject.FindWithTag("Player").transform;
        _enemyTracker = GameObject.FindWithTag("Player").GetComponent<EnemyTracker>();
    }

    private void OnEnable()
    {
        _enemyTracker.Enemies.Add(this.gameObject);
    }

    private void OnDisable()
    {
        _enemyTracker.Enemies.Remove(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDirection();
        MoveToTarget(dir, Speed);
    }

    //Calculates the direction between the Target and this transform
    private void CalculateDirection()
    {
        dir = TargetPos.position - transform.position;
        dir.Normalize();
    }

    //Moves the transform towards the target
    private void MoveToTarget(Vector3 direction, float speed)
    {
        transform.Translate((direction * speed) * Time.deltaTime);
    }

    private void TakeDamage(int amount)
    {
        Health -= amount;
        if(Health <= 0)
        {
            //death routine
        }
    }
}
