using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : MonoBehaviour {

    public float speed;
    public int health;
    private PlayerActor player;
    //private CharacterController controller;

    private UnityEngine.AI.NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerActor>();
        //controller = gameObject.GetComponent<CharacterController>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 dirToPlayer;
        //dirToPlayer = player.transform.position - this.transform.position;
        //dirToPlayer.Normalize();

        //this.transform.position += dirToPlayer * speed * Time.deltaTime;
        //controller.Move(((dirToPlayer) * speed) * Time.deltaTime);

        agent.destination = player.transform.position;
    }

    // Handles damaging this enemy
    public void TakeDamage(GameObject damager, int damage)
    {
        int dmg = damage;
        OnDamage(damager, ref dmg);
        
        health -= dmg;

        if (health < 0)
            Kill(damager);
    }

    protected virtual void OnDamage(GameObject damager, ref int damage)
    {
        //TODO: Extra stuff to be done when taking damage, as an example, damage reduction
        //damage -= 1;
    }

    /// <summary>
    /// Handles destruction of this enemy
    /// </summary>
    /// <param name="killer"></param>
    public void Kill(GameObject killer)
    {
        OnKilled(killer);

        Destroy(this.gameObject);
    }

    protected virtual void OnKilled(GameObject killer)
    {
        //TODO: Any stuff to be done before being destroyed is done here
    }
}
