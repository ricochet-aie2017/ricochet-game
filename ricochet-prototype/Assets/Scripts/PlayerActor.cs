using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour {
    // TODO: Create a weapon system
    public AudioClip ShootSound = null; // Sound to play when shooting
	public GameObject Projectile = null; // The projectile to spawn
	public GameObject Barrel = null; // The spot to spawn projectiles from
    public float recoilFactor = 1.0f; // Amount of recoil, TODO: Maybe make a weapon class to put this code in instead?
    public float TimeBetweenShots = 0.5f; // Actual time between shooting
    public float SpeedFalloff = 0.99f; // How quickly the movement speed from shooting falls off(closer to 1.0 means less friction)
	private float shootTimer = 0.0f; // Countdown timer until the next shot can be fired

    //public float speed = 10.0f;

    //private CharacterController controller;
    //private Vector3 mpos;
    private Vector3 moveDirection; // Bringing this back to do more raw movement using the character controller
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

        /* Should no longer need this
        RaycastHit info;
        if (Input.GetMouseButton(0) && Physics.Raycast(fire_ray, out info))
        {
            if(info.collider.tag == "Enemy")
            {
                Destroy(info.collider.gameObject);
            }
        }*/
        
        // Movement
        GetComponent<CharacterController>().Move((moveDirection * recoilFactor) * Time.deltaTime);
        moveDirection *= SpeedFalloff;

        // Update timer
        if (shootTimer > 0)
            shootTimer -= Time.deltaTime;
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
			return;

        // Play the audio shoot sound.
        GetComponent<AudioSource>().PlayOneShot(ShootSound);

		var proj = GameObject.Instantiate<GameObject>(Projectile);
		proj.transform.position = Barrel.transform.position;
		proj.transform.rotation = Barrel.transform.rotation;
		proj.GetComponent<Rigidbody>().AddForce(Barrel.transform.forward * 1000);
		shootTimer += TimeBetweenShots;

        // Do the knockback movement here
        moveDirection = -(Barrel.transform.forward.normalized * recoilFactor);
        
        /*RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
            Vector3 unit = (hit.point - this.transform.position).normalized;
            Vector3 destination = this.transform.position + ((-unit) * recoilFactor);
            // hit.point: +ve = move towards, -ve = move away
            //agent.destination = destination;
            GetComponent<CharacterController>().Move(((-unit) * recoilFactor));
            //print(agent.destination);
        }*/
    }
}
