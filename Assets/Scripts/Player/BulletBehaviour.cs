using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _offset = 1f;
    Vector3 dir;
    Vector3 _targetPos, _thisPos;
    float _angle;
    #endregion
    #region Properties
    public GameObject Target { get => _target; set => _target = value; }
    public float Speed { get => _speed; set => _speed = value; }
    #endregion

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();

        transform.right = Target.transform.position - transform.position;
    }

    private void MoveTowardsTarget()
    {
        dir = Target.transform.position - transform.position;
        dir.Normalize();

        transform.Translate((dir * Speed) * Time.deltaTime);
    }
}
