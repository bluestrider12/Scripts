using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlatform : MonoBehaviour {

    private static UIManager uiManager;
    private static bool tileSelected;

    public Image buidTowerUI;

    // Use this for initialization
    void Start () {
        if(uiManager == null)
        {
            tileSelected = false;
            uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void selectTile(bool active)
    {
        if(active)
        {
            tileSelected = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(130, 130, 130, 255);
        }
        else
        {
            tileSelected = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }

    }

    private void OnMouseDown()
    {
        if(!tileSelected)
        {
            selectTile(true);
            uiManager.enableBuildUI(gameObject);
        }

    }
}
