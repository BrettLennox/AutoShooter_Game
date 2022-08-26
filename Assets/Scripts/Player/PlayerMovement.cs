using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private float _speed = 3f;
    private Vector2 _moveDir;
    #endregion
    #region Properties
    public float Speed { get => _speed; private set => _speed = value; }
    private Vector2 MoveDir { get => _moveDir; set => _moveDir = value; }
    #endregion

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        MoveDir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if(MoveDir.sqrMagnitude > 1)
        {
            MoveDir = MoveDir.normalized;
        }


        transform.Translate((MoveDir * Speed) * Time.deltaTime);
    }
}
