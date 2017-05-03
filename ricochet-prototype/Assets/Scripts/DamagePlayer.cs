using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public int playerHealth;
    int damage;

	// Use this for initialization
	void Start () {
        print(playerHealth);
	}
	
    void OnCollisionEnter(Collision _collision)
    {
        //if(_collision.gameObject.tag=="Enemy")
        //if(_collision.gameObject)
        //{
            //playerHealth -= damage;
            //print("Enemy Contact! HP: " + playerHealth);
        print(_collision.gameObject.name);
        //}
    }

	// Update is called once per frame
	void Update () {
		
	}
}
