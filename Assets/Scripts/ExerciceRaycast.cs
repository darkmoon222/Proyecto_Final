using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciceRaycast : MonoBehaviour
{

    public Vector3 origin;
    public Vector3 direction;
    public LayerMask mask;
    public float distance;

    public int numRays;
    public float distanceRays;

    private void FixedUpdate()
    {
        

        Vector3 pos = transform.position + origin;
        int sign = 1;
        for(int i = 0; i < numRays; i++)
        {
            

            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(pos, direction);
            if(Physics.Raycast(ray, out hit, distance, mask))
            {
                Debug.Log(hit.transform.name);
                break;
            }

            pos.x += sign * ((i + 1) * distanceRays);
            sign *= -1;
        }

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Vector3 pos = transform.position + origin;
        int sign = 1;
        for(int i = 0; i < numRays; i++)
        {
           
            Gizmos.DrawRay(pos, direction * distance);
            pos.x += sign * ((i + 1) * distanceRays);
            sign *= -1;
        }
        
    }
}
