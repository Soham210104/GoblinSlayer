using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    public GameObject Health;
    public float spawnInterval = 3f;
    public GameObject[] Healths;
    public moveByTouch player;
    public bool isHealth = true;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnHealth", 0f, spawnInterval);
    }

    // Update is called once per frame
    void SpawnHealth()
    {
        if (!player.playerIsDead && isHealth)
        {
            int randomPosition = Random.Range(0, Healths.Length);
            Instantiate(Health, Healths[randomPosition].transform.position, Quaternion.identity);
            isHealth = false;
        }
    }
}
