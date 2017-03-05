using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnActor : MonoBehaviour {

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public float spawn_time; // seconds between spawns
    //public float spawn_radius; // distance from player to spawn

    private PlayerActor player;
    private float spawn_timer; // timer that counts and controls spawning

	// Use this for initialization
	void Start () {
        spawn_timer = Random.Range(spawn_time, 20);
        player = GameObject.FindObjectOfType<PlayerActor>();
	}
	
	// Update is called once per frame
	void Update () {
        // count our timer down each frame
        spawn_timer -= Time.deltaTime;

        if(spawn_timer < 0)
        {
            // reset the timer
            spawn_timer = Random.Range(spawn_time, 20);

            // pick a random angle in radians and set the spawn point
            //float spawn_angle = Random.Range(0, 2 * Mathf.PI);
            //Vector3 spawn_direction = new Vector3(Mathf.Sin(spawn_angle), 0, Mathf.Cos(spawn_angle));
            //spawn_direction *= spawn_radius;

            //Vector3 spawn_point = player.transform.position + spawn_direction;

            Vector3 spawn_point = this.transform.position;

            float randNum = Random.Range(0, 4);

            // spawn the enemy at the disired location

            if (randNum > 2)
            {
                Instantiate(enemy3, spawn_point, Quaternion.identity);
            }
            else if (randNum <= 2 && randNum > 1)
            {
                Instantiate(enemy2, spawn_point, Quaternion.identity);
            }
            else
            {
                Instantiate(enemy1, spawn_point, Quaternion.identity);
            }
        }
	}
}
