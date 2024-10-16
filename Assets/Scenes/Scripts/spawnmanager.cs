using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{


    public GameObject enemy;
    public GameObject enemy2;
    public GameObject enemy3;
    public float startTime;
    public float spawnRate;
    public float endTime;

    public Vector3 witch; //생성은 x, z가 -50 ~ 120 사이에 존재한다
    public int spawn;
    int flag = 1;
    int nowSpawn = 0;




    void Spawn(int flag)
    {

        int x, z;
        x = Random.Range(-50, 121);
        z = Random.Range(-50, 121);

        if ((x > -10 && x < 60) && z > -5 && z < 50) { } //no spawn

        else
        {
            witch = new Vector3(x, 3, z);
            nowSpawn++;
            switch (flag)
            {
                case 1:
                    Instantiate(enemy, witch, transform.rotation);
                    break;
                case 2:
                    Instantiate(enemy2, witch, transform.rotation);
                    break;
                case 3:
                    Instantiate(enemy3, witch, transform.rotation);
                    break;
                default:
                    break;
            }
        }



    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        startTime += Time.deltaTime;
        if (startTime >= spawnRate && gameManager.instance.enemySpawn > nowSpawn)
        {
            flag = Random.Range(1, 4);
            Spawn(flag);
            
            startTime = 0;
        }
    }
}
