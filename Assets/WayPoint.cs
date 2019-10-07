using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public WayPoint nextWayPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(nextWayPoint != null)
        {
            Gizmos.DrawLine(transform.position + Vector3.up, nextWayPoint.transform.position + Vector3.up);
        }
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
