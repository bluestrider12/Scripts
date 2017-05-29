using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    private Vector3 exitPoint;
    private Transform enemyTransform;
    //private bool traverseEnemy;

    // Use this for initialization
    void Start () {
        //traverseEnemy = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setExitPoint(Vector2 slope)
    {
        //Debug.Log(slope.x + " " + slope.y);
        float x, y;
        if(Mathf.Abs(slope.x) > Mathf.Abs(slope.y))
        {
            x = (gameObject.GetComponent<BoxCollider2D>().size.x / 2f) * (slope.x/Mathf.Abs(slope.x));
            y = x * slope.y / slope.x + transform.position.y;
            x += transform.position.x;
        }
        else if(Mathf.Abs(slope.x) < Mathf.Abs(slope.y))
        {
            y = (gameObject.GetComponent<BoxCollider2D>().size.y / 2f) * (slope.y / Mathf.Abs(slope.y));
            x = y * slope.x / slope.y + transform.position.x;
            y += transform.position.y;
        }
        else
        {
            x = (gameObject.GetComponent<BoxCollider2D>().size.x / 2f) * (slope.x / Mathf.Abs(slope.x)) + transform.position.x;
            y = (gameObject.GetComponent<BoxCollider2D>().size.y / 2f) * (slope.y / Mathf.Abs(slope.y)) + transform.position.y;
        }

        exitPoint = new Vector3(x, y, 0f);

    }

    public Vector3 getExitPoint()
    {
        return exitPoint;
    }


}
