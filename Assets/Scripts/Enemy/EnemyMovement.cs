using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IDamageable<int>, IKillable
{
    #region Variables
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform _targetPos;
    [SerializeField] private int _health;
    [SerializeField] private int _pointValue;
    [SerializeField] List<EnemyData> enemyData;

    Vector3 dir;
    #endregion
    #region Properties
    public EnemyData EnemyData { get => _enemyData; set => _enemyData = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public Transform TargetPos { get => _targetPos; set => _targetPos = value; }
    public int Health { get => _health; private set => _health = value; }
    public int PointValue { get => _pointValue; set => _pointValue = value; }
    public EnemyTracker EnemyTracker { get => _enemyTracker; set => _enemyTracker = value; }
    #endregion
    #region References
    private EnemyTracker _enemyTracker;
    #endregion

    private void Awake()
    {
        TargetPos = GameObject.FindWithTag("Player").transform;
        EnemyTracker = GameObject.FindWithTag("Player").GetComponent<EnemyTracker>();
    }

    private void OnValidate()
    {
        if(TargetPos == null)
            TargetPos = GameObject.FindWithTag("Player").transform;
        if(EnemyTracker == null)
            EnemyTracker = GameObject.FindWithTag("Player").GetComponent<EnemyTracker>();
    }

    private void EnemySetup()
    {

        Health = EnemyData.enemyData.enemyHealth;
        Speed = EnemyData.enemyData.enemySpeed;
        PointValue = EnemyData.enemyData.pointValue;
    }

    private void SelectRandomEnemyData()
    {
        enemyData = Resources.LoadAll<EnemyData>("Game Data/Enemies/").ToList();
        if (enemyData.Count > 0)
        {
            int randomEnemyData = Random.Range(0, enemyData.Count - 1);
            EnemyData = enemyData[randomEnemyData];
        }
        else Debug.LogWarning("No EnemyData in directory");
    }

    private void OnEnable()
    {
        EnemyTracker.Enemies.Add(this.gameObject);
        if (EnemyData == null)
        {
            SelectRandomEnemyData();
            EnemySetup();
        }
        else
        {
            EnemySetup();
        }

    }

    private void OnDisable()
    {
        EnemyTracker.Enemies.Remove(this.gameObject);
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
        dir = dir.normalized;
    }

    //Moves the transform towards the target
    private void MoveToTarget(Vector3 direction, float speed)
    {
        transform.Translate((direction * speed) * Time.deltaTime);
    }

    public void Damage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        GameObject.Find("GameManager").GetComponent<ScoreManager>().IncreaseScore(PointValue);
        //death routine
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var bullet = other.CompareTag("Bullet");
        //var player = other.GetComponent<PlayerHealth>();
        if (bullet)
        {
            Damage(other.GetComponent<BulletBehaviour>().Damage);
            GameObject particleSystem = ObjectPool.SharedInstance.GetPooledObject("BulletImpact_ParticleSystem");
            if (particleSystem != null)
            {
                particleSystem.transform.position = other.transform.position;
                particleSystem.transform.rotation = Quaternion.identity;
                particleSystem.SetActive(true);
            }
            other.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("NOT BULLET");
        }
    }
}
