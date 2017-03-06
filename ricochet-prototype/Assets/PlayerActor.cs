﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour {
	public GameObject Projectile = null; // The projectile to spawn
	public GameObject Barrel = null; // The spot to spawn projectiles from
	public float TimeBetweenShots = 0.5f; // Actual time between shooting
	private float shootTimer = 0.0f; // Countdown timer until the next shot can be fired

    //public float speed = 10.0f;

    //private CharacterController controller;
    //private Vector3 mpos;
    //private Vector3 moveDirection;
    //private Vector3 start;
    //private Vector3 end;

	// Use this for initialization
	void Start () {

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

        RaycastHit info;
        if (Input.GetMouseButton(0) && Physics.Raycast(fire_ray, out info))
        {
            if(info.collider.tag == "Enemy")
            {
                Destroy(info.collider.gameObject);
            }
        }
    }

	// Spawns and shoots a new bullet projectile
	public void Shoot()
	{
		if (Projectile == null || Barrel == null)
		{
			Debug.LogWarning("Player attempting to shoot projectile, but projectile or barrel not set");
			return;
		}

		// Prevent shooting before time
		if (shootTimer > 0)
		{
			shootTimer -= Time.deltaTime;
			return;
		}


		var proj = GameObject.Instantiate<GameObject>(Projectile);
		proj.transform.position = Barrel.transform.position;
		proj.transform.rotation = Barrel.transform.rotation;
		proj.GetComponent<Rigidbody>().AddForce(Barrel.transform.forward * 1000);
		shootTimer += TimeBetweenShots;
	}
}
