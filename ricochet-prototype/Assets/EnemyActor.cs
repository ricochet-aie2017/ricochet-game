using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : MonoBehaviour {

    public float speed;
    public float health;
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
}
