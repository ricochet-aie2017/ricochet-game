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

    public Color[] BounceColours;
    public GameObject BounceEffectPrefab;
    public GameObject BloodEffectPrefab;

    GameObject player;
    PlayerHealth playerHealth;
    

	// Use this for initialization
	void Start ()
	{
        GetComponent<Rigidbody>().AddForce(this.transform.forward * Speed);
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        // Set initial colour
        if (BounceColours.Length > 0)
        {
            // Change the colour based on the bounce colour array
            this.GetComponent<MeshRenderer>().material.color = BounceColours[0];
        }
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
            // Create the blood effect
            if (BloodEffectPrefab)
            {
                var YY = Quaternion.FromToRotation(BloodEffectPrefab.transform.up, Vector3.up);
                var lookRot = Quaternion.LookRotation(collision.contacts[0].normal, Vector3.up);
                lookRot = YY * lookRot;
                var effect = Instantiate<GameObject>(BloodEffectPrefab, collision.contacts[0].point, YY);// Quaternion.Euler(-90.0f, 0.0f, 0.0f));

                // Kill the effect after
                GameObject.Destroy(effect, 0.5f);
            }
            playerHealth.TakeDamage(Damage);
            Kill();
            return;
        }

        // Check for enemy collision
        if (collision.gameObject.tag == "Enemy")
        {
            var enemyScript = collision.gameObject.GetComponent<EnemyActor>();

            // Create the blood effect
            if (BloodEffectPrefab)
            {
                var YY = Quaternion.FromToRotation(BloodEffectPrefab.transform.up, Vector3.up);
                var lookRot = Quaternion.LookRotation(collision.contacts[0].normal, Vector3.up);
                lookRot = YY * lookRot;
                var effect = Instantiate<GameObject>(BloodEffectPrefab, collision.contacts[0].point, YY);// Quaternion.Euler(-90.0f, 0.0f, 0.0f));

                // Kill the effect after
                GameObject.Destroy(effect, 0.5f);
            }

            if (enemyScript != null)
            {
                //enemyScript.TakeDamage(gameObject, Damage + (BounceCount * DamagePerBounce));
                enemyScript.TakeDamage(gameObject, Damage + (BounceCount * DamagePerBounce), BounceCount);

                // Destroy the projectile on enemy hit
                Kill();
                return;
            }
        }

        // Create the bounce effect
        if (BounceEffectPrefab)
        {
            var YY = Quaternion.FromToRotation(BounceEffectPrefab.transform.up, Vector3.up);
            var lookRot = Quaternion.LookRotation(collision.contacts[0].normal, Vector3.up);
            lookRot = YY * lookRot;
            var effect = Instantiate<GameObject>(BounceEffectPrefab, collision.contacts[0].point, YY);// Quaternion.Euler(-90.0f, 0.0f, 0.0f));

            // Kill the effect after
            GameObject.Destroy(effect, 0.5f);
        }

        transform.forward = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        Bounce();
    }

    /// <summary>
    /// Action performed for each projectile bounce
    /// </summary>
    private void Bounce()
    {

        BounceCount++; // Increment the bounce counter

        //TODO: Can add effects here
        if (BounceColours.Length > 0)
        {
            // Generate a colour index based on bounces into the bounce colour array
            int colourIndex = BounceCount >= BounceColours.Length ? BounceColours.Length - 1 : BounceCount;

            // Change the colour based on the bounce colour array
            this.GetComponent<MeshRenderer>().material.color = BounceColours[colourIndex];
        }

        

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
