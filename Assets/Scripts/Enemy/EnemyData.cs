using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data" ,menuName = "GameData/Enemy")]
public class EnemyData : ScriptableObject
{
    [System.Serializable]
    public struct Enemy
    {
        public string enemyName;
        public int enemyId;
        public int enemyHealth;
        public int enemyDamage;
        public float enemySpeed;
        public int pointValue;
    }

    public Enemy enemyData;
}
