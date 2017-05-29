using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Vector2 turretOrigin = new Vector2(1, 0);
    private List<GameObject> enemies;
    private GameObject turretSprite;
    private int target = 0;

	// Use this for initialization
	protected void Start () {
        turretSprite = transform.GetChild(0).gameObject;
        enemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	protected void Update () {
        if (enemies.Count > 0 && target < enemies.Count)
        {
            getAngle(transform.InverseTransformPoint(enemies[target].transform.position));
        }
    }

    protected void getAngle(Vector2 enemyPos)
    {
        
        float temp = turretOrigin.x * enemyPos.x + turretOrigin.y* enemyPos.y;
        float a = Mathf.Sqrt(Mathf.Pow(turretOrigin.x, 2) + Mathf.Pow(turretOrigin.y, 2));
        float b = Mathf.Sqrt(Mathf.Pow(enemyPos.x, 2) + Mathf.Pow(enemyPos.y, 2));
        float divisor = a * b;
        float result = Mathf.Acos(temp / divisor)*Mathf.Rad2Deg;
        if(enemyPos.y <=0)
        {
            result *= -1;
        }
        if (result < 0)
        {
            result += 360;
        }
        //Debug.Log(result);
        if ((turretSprite.transform.localEulerAngles.z <= result-5 && result - turretSprite.transform.localEulerAngles.z <180) ||
            (turretSprite.transform.localEulerAngles.z >= result + 5 && (360-turretSprite.transform.localEulerAngles.z + result <180)))
        {
            //Debug.Log("hello " + turretSprite.transform.localEulerAngles.z +" " + result);
            turretSprite.transform.Rotate(Vector3.forward*2);
        }
        else if((turretSprite.transform.localEulerAngles.z >= result+5 && turretSprite.transform.localEulerAngles.z - result <180) ||
            (turretSprite.transform.localEulerAngles.z <= result - 5 && (360-result + turretSprite.transform.localEulerAngles.z < 180)))
        {
            //Debug.Log("hello! " + turretSprite.transform.localEulerAngles.z + " " + result);
            turretSprite.transform.Rotate(Vector3.back*2);
        }
        else
        {
            turretSprite.transform.localEulerAngles = new Vector3(0, 0, result);
        }

    }

    protected void triggerEnter(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }

    protected void triggerExit(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject == enemies[target])
            {
                enemies.Remove(other.gameObject);
            }
            else
            {
                enemies.Remove(other.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject == enemies[target])
            {
                enemies.Remove(other.gameObject);
            }
            else
            {
                enemies.Remove(other.gameObject);
            }
        }
    }

}
