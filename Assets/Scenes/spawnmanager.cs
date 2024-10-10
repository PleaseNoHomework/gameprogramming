using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    public GameObject enemy;
    public float startTime;
    public float spawnRate;
    public Vector3 witch;
    public int spawn;
    int nowSpawn = 0;
    void Spawn()
    {
        Instantiate(enemy, witch, transform.rotation);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
        if (startTime >= spawnRate && nowSpawn < spawn)
        {
            Spawn();
            nowSpawn++;
            startTime = 0;
        }
    }
}
