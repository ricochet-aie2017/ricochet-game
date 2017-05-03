using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour {
    // TODO: Create a weapon system
    //public AudioClip ShootSound = null; // Sound to play when shooting
	public GameObject Projectile = null; // The projectile to spawn
	public GameObject Barrel = null; // The spot to spawn projectiles from
    public GameObject AttachedWeapon = null; // The Model for the current weapon
    //public float recoilFactor = 1.0f; // Amount of recoil, TODO: Maybe make a weapon class to put this code in instead?
    //public float TimeBetweenShots = 0.5f; // Actual time between shooting
    public float SpeedFalloff = 0.99f; // How quickly the movement speed from shooting falls off(closer to 1.0 means less friction)
<<<<<<< HEAD
	private float shootTimer = 0.0f; // Countdown timer until the next shot can be fired
    public float health = 100.0f;
    public int kills = 0;
    public int killPoints = 0;
=======

    // Weapon System
    private WeaponDatabase weaponDB;
    public int SelectedWeapon = 0;
    public int SwapToWeapon = 0;
    public bool DoingSwap = false;
    public bool DoingReload = false;

    // Countdown timers
    private float shootTimer = 0.0f; // Countdown timer until the next shot can be fired
    private float reloadTimer = 0.0f;
    private float swapFromTimer = 0.0f;
    private float swapToTimer = 0.0f;


    private AudioSource audioSource = null;
>>>>>>> refs/remotes/origin/master

    //public float speed = 10.0f;

    //private CharacterController controller;
    //private Vector3 mpos;
    private Vector3 moveDirection; // Bringing this back to do more raw movement using the character controller
    //private Vector3 start;
    //private Vector3 end;

	// Use this for initialization
	void Start()
    {
        // Cache various Components
        audioSource = GetComponent<AudioSource>();

        // Load weapon database
        weaponDB = Resources.Load<WeaponDatabase>("Databases/WeaponDatabase");

        if (weaponDB == null)
            Debug.Log("Failed to load weapon database");
        else
            Debug.Log("Loaded weapon database successfully");

        AttachWeapon(weaponDB.WeaponList[SelectedWeapon].WeaponPrefab);

        // Make sure to set up the proper values for each weapon, as the database saves the values
        foreach (var weapon in weaponDB.WeaponList)
        {
            if (weapon.IsUnlimitedClips)
            {
                weapon.Ammo = weapon.ClipSize;
            }
            else
            {
                weapon.Ammo = 0;
                weapon.AmmoInClips = 0;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 mouse_pos = Input.mousePosition;
        Ray mouse_ray = Camera.main.ScreenPointToRay(mouse_pos);
        Plane player_plane = new Plane(Vector3.up, transform.position);

        float ray_distance = 0;
        player_plane.Raycast(mouse_ray, out ray_distance);

        Vector3 cast_point = mouse_ray.GetPoint(ray_distance);

        Vector3 to_cast_point = cast_point - transform.position;
        to_cast_point.Normalize();
        Ray fire_ray = new Ray(transform.position, to_cast_point);

        // Handle the players input
        HandlePlayerInput();

        Weapon currentWeapon = weaponDB.WeaponList[SelectedWeapon];

        // Movement
        // Make sure we have a valid weapon
        if (SelectedWeapon < weaponDB.WeaponList.Count && SelectedWeapon >= 0)
        {
            GetComponent<CharacterController>().Move((moveDirection * currentWeapon.RecoilFactor) * Time.deltaTime);
            moveDirection *= SpeedFalloff;
        }

        // Update timers
        // TODO: This would be better served being a State Machine, this is just messy
        if (shootTimer > 0)
            shootTimer -= Time.deltaTime;
        if (reloadTimer > 0)
            reloadTimer -= Time.deltaTime;
        else if (DoingReload)
        {
            // Now we actually do the reload
            var ammoToReload = currentWeapon.ClipSize - currentWeapon.Ammo;

            // Handle unlimited AmmoInClips guns
            if (currentWeapon.IsUnlimitedClips)
            {
                currentWeapon.Ammo += ammoToReload;
            }
            else
            {
                if (ammoToReload > currentWeapon.AmmoInClips)
                    ammoToReload = currentWeapon.AmmoInClips;

                currentWeapon.Ammo += ammoToReload;
                currentWeapon.AmmoInClips -= ammoToReload;
            }

            DoingReload = false;
        }
        if (swapFromTimer > 0)
            swapFromTimer -= Time.deltaTime;
        else if (DoingSwap)
        {
            // Do the weapon swap here
            SelectedWeapon = SwapToWeapon;
            DoingSwap = false;
            //TODO: Base time on animation
            swapToTimer = 0.25f;

            currentWeapon = weaponDB.WeaponList[SelectedWeapon];

            // Swap the model
            AttachWeapon(currentWeapon.WeaponPrefab);

            if (currentWeapon.SwapInAnim != null)
            {
                // TODO: Animations
            }
            if (currentWeapon.SwapInSound != null)
            {
                audioSource.PlayOneShot(currentWeapon.SwapInSound);
            }
        }
        if (swapToTimer > 0)
            swapToTimer -= Time.deltaTime;
    }

    // I thought about making an input class to handle all inputs, that still
    // might be worth doing, but for now I am handling all player inputs here
    public void HandlePlayerInput()
    {
        // Shoot
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }

        // Reload
        if (Input.GetMouseButtonDown(1))
        {
            Reload();
        }

        // Swap Weapon
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            int swapTo = SelectedWeapon + (int)(scroll);
            SwapWeapon(swapTo);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapWeapon(1);
        }
    }

    public void AttachWeapon(GameObject weaponPrefab)
    {
        if (AttachedWeapon != null)
            Destroy(AttachedWeapon);

        AttachedWeapon = Instantiate(weaponPrefab);
        var localPos = AttachedWeapon.transform.localPosition;
        var localRot = AttachedWeapon.transform.localRotation;
        AttachedWeapon.transform.SetParent(Barrel.transform);
        AttachedWeapon.transform.localPosition = localPos;
        AttachedWeapon.transform.localRotation = localRot;
    }

    // Spawns and shoots a new bullet projectile
    public void Shoot()
    {
        if (Projectile == null || Barrel == null)
        {
            Debug.LogWarning("Player attempting to shoot projectile, but projectile or barrel not set");
            return;
        }

        if (SelectedWeapon >= weaponDB.WeaponList.Count)
        {
            Debug.LogWarning("Player attempting to shoot but selected weapon out of range");
            return;
        }

        // Make sure the Projectile has the bullet script attached
        if (Projectile.GetComponent<Bullet>() == null)
        {
            Debug.LogWarning("Projectile does not have the Bullet script attached");
            return;
        }

        // Prevent shooting before time
        if (shootTimer > 0 || reloadTimer > 0 || swapFromTimer > 0 || swapToTimer > 0 || DoingReload || DoingSwap)
            return;


        // Use the new Weapon class
        Weapon currentWeapon = weaponDB.WeaponList[SelectedWeapon];


        // Ammo management
        if (!currentWeapon.IsUnlimitedAmmo) // Only manage ammo if this is an unlimited ammo gun
        {
            if (currentWeapon.Ammo == 0) // In theory it should never reach here, but just in case
            {
                return;
            }

            currentWeapon.Ammo--;
        }


        if (currentWeapon.ShootAnim != null)
        {
            // TODO: Animations
        }
        if (currentWeapon.ShootSound != null)
            audioSource.PlayOneShot(currentWeapon.ShootSound);

        var proj = GameObject.Instantiate<GameObject>(Projectile);
        proj.transform.position = Barrel.transform.position;
        proj.transform.rotation = Barrel.transform.rotation;
        shootTimer = currentWeapon.FireRate;

        moveDirection = -(Barrel.transform.forward.normalized * currentWeapon.RecoilFactor);

        // Pass across the weapon values to the bullet
        Bullet bullet = proj.GetComponent<Bullet>();
        bullet.Damage = currentWeapon.BaseDamage;
        bullet.DamagePerBounce = currentWeapon.DamagePerRicochet;
        bullet.MaxBounces = currentWeapon.MaxRicochet;
        bullet.Speed = currentWeapon.MuzzleVelocity;
        bullet.SpeedPerBounce = currentWeapon.SpeedPerRicochet;

        if (currentWeapon.Ammo == 0) // No ammo in the clip, auto reload
        {
            Reload();
        }
    }

    public void Reload()
    {
        if (SelectedWeapon >= weaponDB.WeaponList.Count)
        {
            Debug.LogWarning("Player attempting to reload but selected weapon out of range");
            return;
        }

        Weapon currentWeapon = weaponDB.WeaponList[SelectedWeapon];

        // Prevent reload if no clips or already full clip
        // Also prevent reload if ammo is unlimited(no reload necessary if it doesn't require ammo)
        if (currentWeapon.IsUnlimitedAmmo || (!currentWeapon.IsUnlimitedClips && currentWeapon.AmmoInClips == 0) || currentWeapon.Ammo == currentWeapon.ClipSize)
            return;

        // TODO: This should be synced to the reload animation
        reloadTimer = 1.0f;
        DoingReload = true;

        // Play animations and audio if they exist in the weapon
        if (currentWeapon.ReloadAnim != null)
        {
            // TODO: Animations
        }
        if (currentWeapon.ReloadSound != null)
        {
            audioSource.PlayOneShot(currentWeapon.ReloadSound);
        }
    }

    public void SwapWeapon(int SwapTo)
    {
        // Instead of checking against the weapon list count, we wrap the value
        if (SwapTo < 0) // Negatives
        {
            // TODO: This will have issues if the negative number is greater than the weapon count
            // This shouldn't happen, but if it does, this code will not select the correct weapon
            // At least it will remain within the bounds of the list
            SwapTo = (SwapTo + weaponDB.WeaponList.Count) % weaponDB.WeaponList.Count;
        }
        else if (SwapTo > weaponDB.WeaponList.Count)
        {
            SwapTo = SwapTo % weaponDB.WeaponList.Count;
        }


        Weapon currentWeapon = weaponDB.WeaponList[SelectedWeapon];

        if (currentWeapon.SwapOutAnim != null)
        {
            // TODO: Animations
        }
        if (currentWeapon.SwapOutSound != null)
        {
            audioSource.PlayOneShot(currentWeapon.SwapOutSound);
        }

        // TODO: Base this off weapon swap from anim
        swapFromTimer = 0.25f;
        SwapToWeapon = SwapTo;
        DoingSwap = true;
    }
}
