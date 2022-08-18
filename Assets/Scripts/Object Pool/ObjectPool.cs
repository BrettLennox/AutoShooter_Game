using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    [System.Serializable]
    public struct ObjectPoolData
    {
        public List<GameObject> pooledObjects;
        public GameObject objectToPool;
        public int amountToPool;
    }

    public List<ObjectPoolData> _objectPoolData = new List<ObjectPoolData>();

    private void Awake()
    {
        SharedInstance = this;

        GameObject pooledParent = new GameObject("Pooled Objects");
        for (int i = 0; i < _objectPoolData.Count; i++)
        {
            GameObject tmp;
            for (int y = 0; y < _objectPoolData[i].amountToPool; y++)
            {
                tmp = Instantiate(_objectPoolData[i].objectToPool, pooledParent.transform);
                tmp.SetActive(false);
                _objectPoolData[i].pooledObjects.Add(tmp);
            }
        }
    }

    public GameObject GetPooledObject(string objectName)
    {
        for (int i = 0; i < _objectPoolData.Count; i++)
        {
            if (_objectPoolData[i].objectToPool.name == objectName)
            {
                for (int x = 0; x < _objectPoolData[i].amountToPool; x++)
                {
                    if (!_objectPoolData[i].pooledObjects[x].activeInHierarchy)
                    {
                        return _objectPoolData[i].pooledObjects[x];
                    }
                }
            }
        }

        return null;
    }
}
