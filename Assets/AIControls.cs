using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControls : MonoBehaviour
{
    public bool CanMove;
    public WayPoint nextWayPoint;
    public Vector3 nextSubWayPoint;
    public RacerScript racerScript;

    // Start is called before the first frame update
    void Start()
    {
        racerScript = GetComponent<RacerScript>();
        CanMove = true;
        nextSubWayPoint = GetRandomSubWayPoint(nextWayPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if(CanMove)
        {
            Move();
        }
    }

    void Move()
    {
        racerScript.Accelerate();
        racerScript.TurnTowards(nextSubWayPoint);
        if(Vector3.Distance(transform.position, nextSubWayPoint) <= 0.25f)
        {
            nextWayPoint = nextWayPoint.nextWayPoint;
            nextSubWayPoint = GetRandomSubWayPoint(nextWayPoint);
        }
    }

    Vector3 GetRandomSubWayPoint(WayPoint wayPoint)
    {
        Vector3 randomCircle = Random.insideUnitCircle;
        nextSubWayPoint = nextWayPoint.transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        return nextSubWayPoint;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(nextSubWayPoint, 0.25f);
    }
}
