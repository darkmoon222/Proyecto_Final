using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{

    public Vector3 origin;
    public Vector3 direction;
    public LayerMask mask;
    public float distance;

    private void FixedUpdate()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(transform.position + origin, direction);
        if(Physics.Raycast(ray, out hit, distance, mask))
        {
            Debug.Log(hit.transform.name);
        }
        
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + origin, direction * distance);
    }
}
