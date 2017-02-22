using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActor : MonoBehaviour {

    public Transform target;

    //private Vector3 boom;
    private Vector3 cameraOffset = new Vector3(0, 30, 0);

    public Transform startMarker;
    public Transform endMarker;
    public float speed = 1.0f;
    private float startTime;
    private float journeyLength;

    // Use this for initialization
    void Start()
    {
        // get the vector from the target to us
        //boom = this.transform.position - target.position;

        // initialising for lerp
        startMarker = this.transform;
        endMarker = target;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position + cameraOffset);
    }

    // Update is called once per frame
    void Update()
    {
        // set our position to be the same relative to the player
        //Vector3 target_pos = target.position + boom;
        //this.transform.position = target_pos;

        // lerp
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        this.transform.position = Vector3.Lerp(startMarker.position, endMarker.position + cameraOffset, fracJourney);
    }
}
