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
        if (collision.gameObject.layer == 12)
        {
            Destroy(gameObject);
        }
        
        else if (collision.gameObject.layer == 11) //building
        {
            moveDirection = new Vector3(-moveDirection.x, 0, -moveDirection.z);
        }
    }
}
