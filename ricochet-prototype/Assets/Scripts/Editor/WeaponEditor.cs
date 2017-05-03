using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeaponDatabase))]
public class WeaponEditor : Editor
{
    WeaponDatabase db;

    private void OnEnable()
    {
        db = (WeaponDatabase)target;

        
    }

    //public override void OnInspectorGUI()
    //{
    //    GUILayout.Label("We did it");
    //}


    void AddWeapon()
    {
        db.WeaponList.Add(new Weapon());
    }

    void RemoveWeapon(int index)
    {
        db.WeaponList.RemoveAt(index);
    }

    public void SaveDatabase()
    {
        
    }


    [MenuItem("Ricochet/Create Weapon Database")]
    static void CreateDatabase()
    {
        WeaponDatabase wdb = ScriptableObject.CreateInstance<WeaponDatabase>();
        AssetDatabase.CreateAsset(wdb, "Assets/Resources/Databases/WeaponDatabase.asset");
    }
}
