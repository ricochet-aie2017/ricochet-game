using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    int BounceCount = 0;
    public int MaxBounces = 5;
    public int DamagePerBounce = 5;
    public int Damage = 10;
    

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
        float delta = (float)BounceCount / MaxBounces;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check for max bounces
        if (BounceCount >= MaxBounces) // Reached the maximum number of bounces
        {
            Destroy(this.gameObject);
        }

        // Check for player collision
        if (collision.gameObject.name == "Player") // Hit the player
        {
            Destroy(this.gameObject);
        }

        // Check for enemy collision
        if (collision.gameObject.tag == "Enemy")
        {
            var enemyScript = collision.gameObject.GetComponent<EnemyActor>();

            if (enemyScript != null)
            {
                enemyScript.TakeDamage(gameObject, Damage + (BounceCount * DamagePerBounce));
            }
        }

        BounceCount++; // Increment the bounce counter
    }
}
