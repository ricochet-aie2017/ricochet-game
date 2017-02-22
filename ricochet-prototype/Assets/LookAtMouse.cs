using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, 1, Input.mousePosition.z);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);

        lookPos = lookPos - player.transform.position;
        float angle = Mathf.Atan2(lookPos.z, lookPos.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
    }
}
