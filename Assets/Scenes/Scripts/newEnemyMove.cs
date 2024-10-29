using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newEnemyMove : MonoBehaviour
{
    private enum State
    {
        Idle,
        Move2Nexus,
        FindPlayer,
        Stop
    }

    public int flag;
    private Vector3 moveDirection = Vector3.zero;
    public float speed;
    private float times;
    private float afterSpawnTime;
    public float stopTime;
    private State _state;

    GameObject nexus;
    GameObject player;

    void move(int flag, float time)
    {
        switch (flag)
        {
            case 1:
                aiMove1(time);
                break;
            case 2:
                aiMove2(time);
                break;
            case 3:
                aiMove3(time);
                break;
        }
    }

    void setRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    void setDirection(Vector3 point)
    {
        moveDirection = (point - gameObject.transform.position).normalized;
        moveDirection.y = 0;
    }

    void aiMove1(float time)
    {
        transform.Translate(moveDirection * speed * time);
    }

    void aiMove2(float time) {
        if (times > 0.7f)
        {
            times = 0;
            moveDirection = new Vector3(-moveDirection.x, 0, -moveDirection.z);
        }
        transform.Translate(moveDirection * speed * time);
    }
    void aiMove3(float time) {
        if (times > 1f)
        {
            times = 0;
            setRandomDirection();
        }
        transform.Translate(moveDirection * speed * time);
    }

    void toNexus(float time) {
        setDirection(nexus.transform.position);
        transform.Translate(moveDirection * speed * time);
    }

    void toPlayer(float time) {
        setDirection(player.transform.position);
        transform.Translate(moveDirection * speed * time * 1.3f);
    }


    //1 : nexus 2 : player 3 : stop
    public void changeState(int flag)
    {
        switch(flag)
        {
            case 1:
                _state = State.Move2Nexus;
                break;
            case 2:
                _state = State.FindPlayer;
                break;
            case 3:
                _state = State.Stop;
                break;
        }
    }


    
    // Start is called before the first frame update
    void Start()
    {
        times = 0;
        afterSpawnTime = 0;
        stopTime = 0;
        _state = State.Idle;
        setRandomDirection();
        nexus = GameObject.Find("Factory");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        times += Time.deltaTime;
        afterSpawnTime += Time.deltaTime;
        
        if (afterSpawnTime > 8f && _state == State.Idle)
        {
            _state = State.Move2Nexus;
        }

        switch (_state)
        {
            case State.Idle:
                move(flag, Time.deltaTime);
                break;
            case State.Move2Nexus:
                toNexus(Time.deltaTime);
                break;
            case State.FindPlayer:
                toPlayer(Time.deltaTime);
                break;
            case State.Stop:
                stopTime += Time.deltaTime;
                if (stopTime >= 2)
                { 
                    _state = State.Move2Nexus;
                    stopTime = 0;
                }
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            moveDirection = new Vector3(-moveDirection.x, 0, -moveDirection.z);
        }

        if (collision.gameObject.tag == "UpgradePoint") // nexus
        {
            Debug.Log("collision Nexus!!!");
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player") //Lost player Life;
        {
            Debug.Log("collision Player!!!");
            Destroy(gameObject);
        }


    }
}
