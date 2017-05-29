using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    public GameObject tower;

    private Camera mainCamera;
    private Image buildUI;
    private Button buildButton;
    private Vector3 towerPos;
    private TowerPlatform selectedTowerPlatform;
    private bool setTodeactivate;


    // Use this for initialization
    void Start () {

        mainCamera = Camera.main;
        buildUI = gameObject.transform.GetChild(0).GetComponent<Image>();
        buildButton = buildUI.transform.GetChild(0).GetComponent<Button>();
        transform.GetChild(0).gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        detectClick();
 
	}

    void detectClick()
    {
        if (setTodeactivate && (Input.GetMouseButtonDown(0) && !RectTransformUtility.RectangleContainsScreenPoint(
         buildUI.rectTransform, Input.mousePosition, null)))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            selectedTowerPlatform.selectTile(false);
            setTodeactivate = false;

        }
        else if(transform.GetChild(0).gameObject.activeSelf)
        {
            setTodeactivate = true;
        }
    }

    public void enableBuildUI(GameObject towerPlatform)
    {
        selectedTowerPlatform = towerPlatform.GetComponent<TowerPlatform>();
        towerPos = towerPlatform.transform.position;
        transform.GetChild(0).gameObject.SetActive(true);
        Vector3 viewPos = mainCamera.WorldToViewportPoint(towerPos);
        if (viewPos.x >= 0.5f)
        {
            buildUI.rectTransform.anchoredPosition = new Vector2(180, 0);
        }
        else
        {
            buildUI.rectTransform.anchoredPosition = new Vector2(620, 0);
        }
    }

    public void buildTower(int towerType)
    {
        if(towerPos != null)
        {
            Instantiate(tower, towerPos, Quaternion.identity);
            setTodeactivate = false;
            transform.GetChild(0).gameObject.SetActive(false);
            selectedTowerPlatform.selectTile(false);
        }
    }
}
