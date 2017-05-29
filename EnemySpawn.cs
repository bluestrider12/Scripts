using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public GameObject enemy;

    private int startWait = 1;
    private int spawnWait = 4;
    private int waveWait = 10;

    private Vector3 spawnPosition;
    private int enemyCount = 20;

	// Use this for initialization
	void Start () {
        spawnPosition = transform.position;

        StartCoroutine(spawnEnemies());
    }


    IEnumerator spawnEnemies()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemy, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
