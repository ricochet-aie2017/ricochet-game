using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : MonoBehaviour {

    public float speed;
    private PlayerActor player;
    //private CharacterController controller;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerActor>();
        //controller = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dirToPlayer;
        dirToPlayer = player.transform.position - this.transform.position;
        dirToPlayer.Normalize();

        this.transform.position += dirToPlayer * speed * Time.deltaTime;
        //controller.Move(((dirToPlayer) * speed) * Time.deltaTime);
	}
}
