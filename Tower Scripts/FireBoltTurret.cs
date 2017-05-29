using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoltTurret : Turret {

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        triggerEnter(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        triggerExit(other);
    }

}
