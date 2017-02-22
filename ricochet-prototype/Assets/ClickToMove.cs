﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public float recoilFactor = 1.0f;

    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                Vector3 unit = (hit.point - this.transform.position).normalized;
                Vector3 destination = this.transform.position + ((-unit) * recoilFactor);
                // hit.point: +ve = move towards, -ve = move away
                agent.destination = destination;
                //print(agent.destination);
            }
        }
    }
}
