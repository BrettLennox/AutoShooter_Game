using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    #region Variables
    [SerializeField] List<GameObject> _enemies;
    #endregion
    #region Properties
    public List<GameObject> Enemies { get => _enemies; set => _enemies = value; }
    #endregion

    private void Update()
    {
        if (Enemies.Count == 0)
        {
            //foreach (GameObject bullet in GetComponent<PlayerAttack>().Bullets)
            //{
            //    bullet.gameObject.SetActive(false);
            //}
        }
    }

    public GameObject ClosestEnemy()
    {
        if (Enemies.Count > 0)
        {
            Enemies.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position)));
            return Enemies[0];
        }
        return null;
    }
}
