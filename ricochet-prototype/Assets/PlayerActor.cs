using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour {

    public float speed = 10.0f;

    private CharacterController controller;
    private Vector3 mpos;
    private Vector3 moveDirection;
    private Vector3 start;
    private Vector3 end;

	// Use this for initialization
	void Start () {
        controller = gameObject.GetComponent<CharacterController>();
        mpos = new Vector3(0, 0, 0);
        moveDirection = new Vector3(0, 0, 0);
        start = this.transform.position;
        end = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        start = this.transform.position;

        moveDirection = end - start;
        moveDirection.Normalize();

        if (Vector3.Distance(start, end) > 10)
        {
            controller.Move(((moveDirection) * speed) * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            end = Input.mousePosition;
            print(end);
        }
    }
}
