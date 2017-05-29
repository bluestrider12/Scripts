using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTile : MonoBehaviour {


    public Vector2 position { get; set; }
    private List<Vector2> validNextPaths;


    public void addValidPath(Vector2 validPathPos)
    {
        if (validNextPaths == null)
        {
            validNextPaths = new List<Vector2>();
        }
        validNextPaths.Add(validPathPos);
    }

    public void removeValidPath(Vector2 invalidPath)
    {
        if(validNextPaths !=null)
        {
            validNextPaths.Remove(invalidPath);
        }
    }

    public List<Vector2> getValidPaths()
    {
        return validNextPaths;
    }



}
