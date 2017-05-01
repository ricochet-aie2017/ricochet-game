using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceDisplay : MonoBehaviour
{
    LineRenderer lineRenderer;
    public float MaximumDistance = 100;
    

	// Use this for initialization
	void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update()
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(transform.position);

        float totalDistance = 0;
        Vector3 direction = transform.forward;
        Vector3 pos = transform.position;
        // Layer mask, to check against all colliders except those in the bullet layer
        var layerMask = 1 << 8; // BulletLayer is the 8th layer
        layerMask = ~layerMask;

        while (totalDistance < MaximumDistance)
        {
            // Perform the cast
            RaycastHit hit;
            // TODO: Does the hit still return distance and the point if nothing was hit? Answer: It does not
            if (Physics.Raycast(pos, direction, out hit, MaximumDistance - totalDistance, layerMask))
            {
                positions.Add(hit.point);


                // If the collider is an enemy, then this is the final point
                if (hit.collider.tag == "Enemy")
                    break;
                
                totalDistance += hit.distance;
                pos = hit.point;
                // Get the reflection vector against whatever was hit
                direction = Vector3.Reflect(direction, hit.normal);

            }
            else
            {
                // Ray didn't hit anything, calculate the end point by creating a vector using the remaining distance
                positions.Add((pos + (direction.normalized * (MaximumDistance - totalDistance))));
                break;
            }
        }

        // Update the positions in the line renderer
        lineRenderer.numPositions = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
	}
}
