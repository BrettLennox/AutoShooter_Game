using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;
    Vector3 dir;
    #endregion
    #region Properties
    public GameObject Target { get => _target; set => _target = value; }
    public float Speed { get => _speed; set => _speed = value; }
    #endregion

    // Update is called once per frame
    void Update()
    {
        dir = Target.transform.position - transform.position;
        dir.Normalize();

        transform.Translate((dir * Speed) * Time.deltaTime);
    }
}
