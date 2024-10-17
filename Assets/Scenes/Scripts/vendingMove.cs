using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vendingMove : MonoBehaviour
{
    public float endTime;
    private float startTime;
    private float timeflow = 0;
    public float speed;
    private Vector3 moveDirection;
    public int flag = 0;
    public int destroyFlag = 0;
    //일직선으로 움직임, 방향 랜덤
    void aiMove1() {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }
    //왕복으로 움직임
    void aiMove2() {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized; 
    }

    //1초마다 무작위 방향으로 움직임
    //flag == 3




    // Start is called before the first frame update
    void Start()
    {
        startTime = 0;
        aiMove1();
        if (flag == 2)
        {
            speed *= 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
        startTime += Time.deltaTime;
        timeflow += Time.deltaTime;
        if (flag == 2)
        {
            if(timeflow > 0.7f)
            {
                moveDirection = new Vector3(-moveDirection.x, 0, -moveDirection.z);
                timeflow = 0;
            }
        }

        else if(flag == 3)
        {
            if (timeflow > 1f)
            {
                aiMove1();
                timeflow = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyFlag == 1) return;

        if (collision.gameObject.layer == 9)
        {
            moveDirection = new Vector3(-moveDirection.x, 0, -moveDirection.z);
        }
        else
        {
            destroyFlag = 1;
            if (collision.gameObject.layer == 12 && gameObject.layer == 13) //blue building & blue bullets
            {
                Destroy(gameObject);
                gameManager.instance.score += 100;
                gameManager.instance.enemyKill++;
                Debug.Log("enemy Killed!" + gameManager.instance.enemyKill);
            }

            else if (collision.gameObject.layer == 14 && gameObject.layer == 15) //red vending & red bullets
            {
                Destroy(gameObject);
                gameManager.instance.score += 200;
                gameManager.instance.enemyKill++;
                Debug.Log("enemy Killed!" + gameManager.instance.enemyKill);
            }
            else if (collision.gameObject.layer == 16 && gameObject.layer == 17) //green vending & green bullets
            {
                Destroy(gameObject);
                gameManager.instance.score += 300;
                gameManager.instance.enemyKill++;
                Debug.Log("enemy Killed!" + gameManager.instance.enemyKill);
            }
            else
            {
                destroyFlag = 0;
            }
        }
    }

}
