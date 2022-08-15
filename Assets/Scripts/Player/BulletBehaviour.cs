using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject _target;
    [SerializeField] private int _damage = 2;
    [SerializeField] private float _speed;
    [SerializeField] private float _offset = 1f;
    Vector3 dir;
    Vector3 _targetPos, _thisPos;
    float _angle;
    #endregion
    #region Properties
    public GameObject Target { get => _target; set => _target = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public int Damage { get => _damage; set => _damage = value; }
    #endregion

    // Update is called once per frame
    void Update()
    {

        if (Target != null) { MoveTowardsTarget(); }
        if(GameObject.FindWithTag("Player").GetComponent<EnemyTracker>().ClosestEnemy() != null)
        {
            Target = GameObject.FindWithTag("Player").GetComponent<EnemyTracker>().ClosestEnemy();
        }
        else { this.gameObject.SetActive(false); }
        //else if (Target == null)
        //{
        //    Target = GameObject.FindWithTag("Player").GetComponent<EnemyTracker>().ClosestEnemy();
        //}
    }

    private void MoveTowardsTarget()
    {
        dir = Target.transform.position - transform.position;
        dir.Normalize();

        transform.Translate((dir * Speed) * Time.deltaTime);
    }

    private void OnDisable()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerAttack>().Bullets.Remove(this.gameObject);
    }
}
