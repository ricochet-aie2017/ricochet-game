using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDatabase : ScriptableObject
{
    public List<Weapon> WeaponList = new List<Weapon>();
}
