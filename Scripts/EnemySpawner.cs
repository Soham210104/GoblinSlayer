using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public float spawnRate = 12.0f;//After this time enemies will be spawn...lesser the spawnRate faster they will spawn
    public float spawnTime = 30.0f;//After the spawnTime seconds the rate of the spawner will decreased.
    [SerializeField]
    public GameObject enemyPrefab;

    public moveByTouch playerScript;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawner());
        StartCoroutine(spawnDecrement());
        //Debug.Log("Spawn Rate is" + spawnRate);
    }

    IEnumerator spawnDecrement() 
    {
        while(spawnRate > 0) 
        {
            yield return new WaitForSeconds(spawnTime);//After each particular spawn time the spawn Rate will decrease
            //Thus after each 30 seconds spawn rate will decrease and enemies would spawn more faster
            spawnRate -= 1;
        }
    }
    
    IEnumerator spawner()
    {
        int i = 0;
        while(i<10 && playerScript.playerIsDead == false)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            //Debug.Log("Spawn Rate is" + spawnRate);
            i++;
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
