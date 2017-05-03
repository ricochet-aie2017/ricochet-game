using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    // Merely for the editor
    [SerializeField] public string Name = "";

    // Properties only for the weapon itself
    [SerializeField] public int ClipSize = 0;
    [SerializeField] public float FireRate = 1.0f;
    [SerializeField] public float RecoilFactor = 1.0f;
    [SerializeField] public bool IsUnlimitedAmmo = false; // Makes a weapon with unlimited ammo and no reloading
    [SerializeField] public bool IsUnlimitedClips = false; // Makes a weapon with unlimited clip ammo, but with reloading to ClipSize

    // Properties passed on to the bullet
    [SerializeField] public int BaseDamage = 5;
    [SerializeField] public int DamagePerRicochet = 5;
    [SerializeField] public int MuzzleVelocity = 500;
    [SerializeField] public int SpeedPerRicochet = 500;
    [SerializeField] public int MaxRicochet = 5;

    // Properties that change throughout gameplay
    [SerializeField] public int AmmoInClips = 0;
    [SerializeField] public int Ammo = 6;

    // Properties that contain visual/audio data
    [SerializeField] public GameObject WeaponPrefab;

    [SerializeField] public AudioClip ShootSound;
    [SerializeField] public AudioClip ReloadSound;
    [SerializeField] public AudioClip SwapOutSound;
    [SerializeField] public AudioClip SwapInSound;

    [SerializeField] public AnimationClip ShootAnim;
    [SerializeField] public AnimationClip ReloadAnim;
    [SerializeField] public AnimationClip SwapOutAnim;
    [SerializeField] public AnimationClip SwapInAnim;
}