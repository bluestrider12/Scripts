using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour {

    public GameObject waypoint;
    public GameObject spawnPoint;


    private List<GameObject> waypoints;

    private void Awake()
    {
        waypoints = new List<GameObject>();
    }

    // Use this for initialization
    void Start () {

    }
	

    public void instantiateWaypoint(GameObject[] pathTiles)
    {
        int startPoint = pathTiles.Length-1;
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(spawnPoint, new Vector3(pathTiles[startPoint].transform.position.x+3, pathTiles[startPoint].transform.position.y,0), spawnRotation);
        waypoints.Add(Instantiate(waypoint, pathTiles[startPoint].transform));
        for (int i = startPoint; i >1 ; i--)
        {
            if (pathTiles[i].transform.position.x != pathTiles[i-2].transform.position.x && pathTiles[i].transform.position.y  != pathTiles[i - 2].transform.position.y)
            {
                waypoints.Add(Instantiate(waypoint, pathTiles[i - 1].transform));
            }
        }
        waypoints.Add(Instantiate(waypoint, new Vector3(pathTiles[0].transform.position.x -3, pathTiles[0].transform.position.y, 0), spawnRotation));
        findSlope();
    }

    private void findSlope()
    {
        for(int i =0; i < waypoints.Count-1; i++)
        {
            float x = waypoints[i + 1].transform.position.x - waypoints[i].transform.position.x;
            float y = waypoints[i + 1].transform.position.y - waypoints[i].transform.position.y;
            Vector2 slope = new Vector2(x, y);
            waypoints[i].GetComponent<Waypoint>().setExitPoint(slope);

        }
    }



    public Vector3 getNextWaypoint(int index)
    {
        if (index < waypoints.Count)
        {
            return waypoints[index].GetComponent<Transform>().position;
        }
        else
        {
            return waypoints[waypoints.Count - 1].gameObject.transform.position;
        }
    }
}
