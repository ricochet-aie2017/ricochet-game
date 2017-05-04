using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.25f;     // The time in seconds between each attack.
    //public int attackDamage = 10;               // The amount of health taken away per attack.

    //Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    GameObject enemy;
    PlayerHealth playerHealth;                  // Reference to the player's health.
    PlayerActor playerActor;
    EnemyActor enemyActor;
    //EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.

    CombatMusicControl musicControl;

    void Awake ()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerActor = player.GetComponent<PlayerActor>();
        enemy = this.gameObject;
        enemyActor = enemy.GetComponent<EnemyActor>();
        //enemyHealth = GetComponent<EnemyHealth>();
        //anim = GetComponent <Animator> ();
        musicControl = player.GetComponent<CombatMusicControl>();
    }

/*    void OnTriggerEnter (Collider other)
    {
        // If the entering collider is the player...
        if(other.gameObject == player)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        // If the exiting collider is the player...
        if(other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }

    void OnCollisionEnter (Collision col)
    {
        // If the entering collider is the player...
        if (col.gameObject.tag == "Player")
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }

    void OnCollisionExit (Collision col)
    {
        // If the exiting collider is the player...
        if (col.gameObject.tag == "Player")
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }*/

    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        float dist = Vector3.Distance(player.transform.position, enemy.transform.position);

        if (dist <= 1.2)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        // Audio transition for quiet/action moments; not working 4 May 2017
        //if(dist <= 10)
        //{
        //    print("In Range Fighting Music");
        //    if (musicControl.fighting == false)
        //    {
        //        print("TransitionIn Action");
        //        musicControl.fighting = true;
        //        musicControl.TransitionAudio(musicControl.fighting);
        //    }
        //}
        //else
        //{
        //    print("Out Range Fighting Music");
        //    if (musicControl.fighting == true)
        //    {
        //        print("TransitionOut Action");
        //        musicControl.fighting = false;
        //        musicControl.TransitionAudio(musicControl.fighting);
        //    }
        //}

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange) //&& enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack ();
        }

        // If the player has zero or less health...
        if(playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            //anim.SetTrigger ("PlayerDead");
        }
    }

    void Attack ()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if(playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            //playerHealth.TakeDamage (attackDamage);
            playerHealth.TakeDamage(enemyActor.damage);
        }
    }
}