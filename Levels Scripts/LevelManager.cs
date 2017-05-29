using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject fieldTile;
    public GameObject pathTile;
    public GameObject wpManager;
    public GameObject towerPlatform;

    private float xStep = 2.4f;
    private float yStep = 1.8f;


    private const int maxX = 9;
    private const int maxY = 7;
    float xOffset = 8.4f;
    float yOffset = -4.5f;
    private GameObject[,] m_tiles;
    private HashSet<Vector2> blockedPathTiles;
    private HashSet<Vector2> invalidPathTiles;
    private Stack<GameObject> generatedPaths;
    private Quaternion spawnRotation;
    private int startPoint;

    private void Awake()
    {
        spawnRotation = Quaternion.identity;
        m_tiles = new GameObject[maxX,maxY];
        blockedPathTiles = new HashSet<Vector2>();
        invalidPathTiles = new HashSet<Vector2>();
        generatedPaths = new Stack<GameObject>();
        
    }
    // Use this for initialization
    void Start () {
        generateMap();
        //hello
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void generatePath()
    {
        int x = 0;
        int y = Random.Range(0, maxY);
        startPoint = y;
        addFirstTile(x, y);
        for (int a =0; a < 100; a++)
        {
            if (x == maxX - 1)
            {
                blockAdjacent(x, y);
                break;
            }
            List<Vector2> nextPositions = getAdjacentTiles(x, y);
            if (nextPositions.Count == 0)
            {
                invalidPathTiles.Add(new Vector2(x, y));
                m_tiles[x, y].GetComponent<PathTile>().removeValidPath(new Vector2(x, y));
                Destroy(generatedPaths.Pop());
                m_tiles[x, y] = null;
                x = (int)generatedPaths.Peek().GetComponent<PathTile>().position.x;
                y = (int)generatedPaths.Peek().GetComponent<PathTile>().position.y;
                List<Vector2> validPaths = m_tiles[x, y].GetComponent<PathTile>().getValidPaths();
                for(int i =0; i< validPaths.Count; i++)
                {
                    blockedPathTiles.Remove(validPaths[i]);
                }
            }
            else
            {
                int genXY = Random.Range(0, nextPositions.Count);
                blockAdjacent(x, y);
                x = (int)(nextPositions[genXY].x);
                y = (int)nextPositions[genXY].y;
                //Debug.Log("Path x: " + x + "y: " + y);
                m_tiles[x, y] = Instantiate(pathTile, new Vector3((xOffset - (xStep * x)), (yOffset + (yStep * y)), 0), spawnRotation);
                m_tiles[x, y].GetComponent<PathTile>().position= nextPositions[genXY];
                generatedPaths.Push(m_tiles[x, y]);
            }
        }
        //Add waypoints
        GameObject[] pathsArray= new GameObject[generatedPaths.Count];
        generatedPaths.CopyTo(pathsArray, 0);
        wpManager.GetComponent<WaypointManager>().instantiateWaypoint(pathsArray);

    }

    private void addFirstTile(int x, int y)
    {
        Vector3 tileLocation = new Vector3((xOffset - (xStep * x)), (yOffset + (yStep * y)), 0);
        blockedPathTiles.Add(new Vector2(x, y));
        m_tiles[x, y] = Instantiate(pathTile, tileLocation, spawnRotation);
        generatedPaths.Push(m_tiles[x, y]);
    }

    private List<Vector2> getAdjacentTiles(int x, int y)
    {
        List<Vector2> nextPositions = new List<Vector2>();
        nextPositions.Add(new Vector2(x, y + 1));
        nextPositions.Add(new Vector2(x - 1, y));
        nextPositions.Add(new Vector2(x, y - 1));
        nextPositions.Add(new Vector2(x + 1, y));

        if (y == maxY - 1 || blockedPathTiles.Contains(new Vector2(x, y + 1)) || (x <=1 && startPoint > y)
            || invalidPathTiles.Contains(new Vector2(x, y+1)))
        {
            nextPositions.Remove((new Vector2(x, y + 1)));
        }
        if (x == 0 || y <= 1 || y>= maxY - 2 || blockedPathTiles.Contains(new Vector2(x - 1, y))
            || invalidPathTiles.Contains(new Vector2(x-1, y)))
        {
            nextPositions.Remove(new Vector2(x - 1, y));
        }
        if (y == 0 || blockedPathTiles.Contains(new Vector2(x, y - 1)) || (x <= 1 && startPoint < y)
            || invalidPathTiles.Contains(new Vector2(x, y - 1)))
        {
            nextPositions.Remove(new Vector2(x, y - 1));
        }
        if (x == maxX - 1 || blockedPathTiles.Contains(new Vector2(x + 1, y))
            || invalidPathTiles.Contains(new Vector2(x + 1, y)))
        {
            nextPositions.Remove(new Vector2(x + 1, y));
        }
        return nextPositions;
    }
    


    private void blockAdjacent(int x, int y)
    {
        List<Vector2> nextPositions = new List<Vector2>();
        nextPositions.Add(new Vector2(x, y + 1));
        nextPositions.Add(new Vector2(x - 1, y));
        nextPositions.Add(new Vector2(x, y - 1));
        nextPositions.Add(new Vector2(x + 1, y));
        for (int i = 0; i < nextPositions.Count; i++)
        {
            if (nextPositions[i].x>=0 && nextPositions[i].x < maxX && nextPositions[i].y >= 0 && nextPositions[i].y < maxY 
                && blockedPathTiles.Add(nextPositions[i]))
            {
                m_tiles[x, y].GetComponent<PathTile>().addValidPath(nextPositions[i]);
            }
        }


    }

    private void generateTowerPlatforms()
    {
        //Add TowerPlatforms
        foreach (Vector2 xyCord in blockedPathTiles)
        {
            if (m_tiles[(int)xyCord.x, (int)xyCord.y] == null)
            {
                m_tiles[(int)xyCord.x, (int)xyCord.y] = Instantiate(towerPlatform, new Vector3((xOffset - (xStep * (int)xyCord.x)), (yOffset + (yStep * (int)xyCord.y)), 0), spawnRotation);
            }
        }
    }

    private void generateFields()
    {
        for (int i = 0; i < maxX; i++)
        {
            for (int k = 0; k < maxY; k++)
            {
                if (m_tiles[i, k] == null)
                {
                    m_tiles[i, k] = Instantiate(fieldTile, new Vector3((xOffset - (xStep * i)), (yOffset + (yStep * k)), 0), spawnRotation);
                }
            }
        }
    }

    private void generateMap()
    {
        generatePath();
        generateTowerPlatforms();
        generateFields();
    }
}
