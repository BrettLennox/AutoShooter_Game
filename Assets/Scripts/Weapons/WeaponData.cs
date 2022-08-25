using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "GameData/Weapon")]
public class WeaponData : ScriptableObject
{
    [System.Serializable]
    public struct Weapon
    {
        public string weaponName;
        public int weaponID;
        public int weaponDamage;
        public float fireRate;
        public float bulletSpeed;
    }

    public Weapon weaponData;
}
