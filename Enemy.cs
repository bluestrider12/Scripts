using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    private WaypointManager waypointManager;
    private int waypointIndex;
    private float speed;
    private bool atWaypont;

    float bezierTime = 0;
    private Vector3 wpExitPoint;
    private Vector3 wpMidPoint;
    private Vector3 wpStartPoint;

	// Use this for initialization
	void Start () {
        waypointManager = GameObject.FindWithTag("WaypointManager").GetComponent<WaypointManager>();
        waypointIndex = 0;
        speed = 1*Time.deltaTime;
        atWaypont = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        navigate();
	}

    private void navigate()
    {
        if(!atWaypont)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypointManager.getNextWaypoint(waypointIndex), speed);
        }
        else
        {
            traverseBelzierCurve(wpExitPoint, wpMidPoint);
        }
    }

    private void traverseBelzierCurve(Vector3 exitPoint, Vector3 midPoint)
    {
        if(bezierTime <= 1)
        {
            //float exitPointDistance = Mathf.Sqrt(Mathf.Pow(exitPoint.x - transform.position.x, 2f) + Mathf.Pow(exitPoint.y - transform.position.y, 2f));
            bezierTime += Time.deltaTime * .9f;
            float x = (((1 - bezierTime) * (1 - bezierTime)) * wpStartPoint.x) + (2 * bezierTime * (1 - bezierTime) * midPoint.x) +
                ((bezierTime * bezierTime) * exitPoint.x);
            float y = (((1 - bezierTime) * (1 - bezierTime)) * wpStartPoint.y) + (2 * bezierTime * (1 - bezierTime) * midPoint.y) +
                ((bezierTime * bezierTime) * exitPoint.y);
            //Debug.Log(bezierTime);
            transform.position = new Vector3(x, y, 0);
        }
        else
        {
            atWaypont = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Waypoint"))
        {
            bezierTime = 0;
            atWaypont = true;
            wpStartPoint = transform.position;
            wpExitPoint = other.gameObject.GetComponent<Waypoint>().getExitPoint();
            wpMidPoint = other.gameObject.transform.position;
            waypointIndex++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Waypoint"))
            atWaypont = false;
    }
}
