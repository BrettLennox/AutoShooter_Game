using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float _spawnTimer = 2f;
    private float timer = Mathf.Infinity;

    [Header("Spawn Data")]
    [SerializeField] private GameObject _spawnParent;
    [SerializeField] private List<GameObject> _spawnLocations;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform spawner in _spawnParent.transform)
        {
            _spawnLocations.Add(spawner.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= _spawnTimer)
        {
            int randomSpawnZone = Random.Range(0, _spawnLocations.Count);
            GameObject enemy = ObjectPool.SharedInstance.GetPooledObject("Enemy");
            if(enemy != null)
            {
                enemy.transform.position = _spawnLocations[randomSpawnZone].transform.position;
                enemy.transform.rotation = Quaternion.identity;
                enemy.SetActive(true);
            }
            timer = 0;
        }
    }
}
