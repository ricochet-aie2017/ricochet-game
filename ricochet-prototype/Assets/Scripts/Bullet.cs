using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bullet : MonoBehaviour
{
    int BounceCount = 0;
    public int MaxBounces = 5;
    public int DamagePerBounce = 10;
    public int Damage = 5;
    public int Speed = 500;
    public int SpeedPerBounce = 500;

    GameObject player;
    PlayerHealth playerHealth;
    

	// Use this for initialization
	void Start ()
	{
        GetComponent<Rigidbody>().AddForce(this.transform.forward * Speed);
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
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
            Kill();
            return;
        }
        

        // Check for player collision
        if (collision.gameObject.name == "Player") // Hit the player
        {
            playerHealth.TakeDamage(Damage);
            Kill();
            return;
        }

        // Check for enemy collision
        if (collision.gameObject.tag == "Enemy")
        {
            var enemyScript = collision.gameObject.GetComponent<EnemyActor>();

            if (enemyScript != null)
            {
                //enemyScript.TakeDamage(gameObject, Damage + (BounceCount * DamagePerBounce));
                enemyScript.TakeDamage(gameObject, Damage + (BounceCount * DamagePerBounce), BounceCount);

                // Destroy the projectile on enemy hit
                Kill();
                return;
            }
        }


        transform.forward = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        Bounce();
    }

    /// <summary>
    /// Action performed for each projectile bounce
    /// </summary>
    private void Bounce()
    {
        //TODO: Can add effects here

        BounceCount++; // Increment the bounce counter

        //TODO: Increase point counter
        //TODO: Increase the projectile speed
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(this.transform.forward * (Speed + (SpeedPerBounce * BounceCount)));
    }

    /// <summary>
    /// Action performed when the projectile is to be destroyed
    /// </summary>
    private void Kill()
    {
        // TODO: Can add effects here
        Destroy(this.gameObject);
    }
}
