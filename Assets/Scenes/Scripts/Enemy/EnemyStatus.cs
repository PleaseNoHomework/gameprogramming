using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move2Nexus,
        FindPlayer,
        Stop,
        Die
    }

    public int HP;
    GameObject nexus;
    GameObject player;
    public GameObject dieEffect;
    public AudioSource dieAudio;

    public int dieFlag = 0;
    public float speed;
    public State _state = State.Idle;
    public Vector3 moveDirection = Vector3.zero;
    public float time = 0;
    public float stopTime = 0;
    public void setRandomDirection()
    {
        float angle = Random.Range(0f, 180f);
        moveDirection = new Vector3(0, angle, 0);
        gameObject.transform.Rotate(moveDirection);
    }
    void setDirection(Vector3 point)
    {
        moveDirection = (point - gameObject.transform.position).normalized;
        moveDirection.y = 0;
    }


    void toTarget(Transform target)
    {
        if (target != null)
        {
            Vector3 enemyPos = transform.position;
            Vector3 targetPos = new Vector3(target.position.x - enemyPos.x, 0, target.position.z - enemyPos.z);

            if (targetPos != Vector3.zero)
            {
                // 목표 회전값 계산
                Quaternion targetRotation = Quaternion.LookRotation(targetPos);

                // y축만 회전 적용
                transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

            }
        }
    }

    IEnumerator die()
    {
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        dieAudio.Play();
        BoxCollider hitbox = gameObject.GetComponent<BoxCollider>();
        if(hitbox != null)
        {
            hitbox.enabled = false;
        }
        yield return new WaitForSeconds(1f);
        dieAudio.Stop();

        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        nexus = GameObject.Find("Factory");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.instance.isGamePaused)
        {
            if (_state == State.Die) { }
            else if (_state != State.Stop) transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
            else //플레이어를 놓친 경우
            {
                stopTime += Time.deltaTime;
                if (stopTime > 1f)
                {
                    stopTime = 0;
                    _state = State.Move2Nexus;
                }
            }
            time += Time.deltaTime;
            if (_state == State.Idle && time >= 8f) //소환하고 움직이다가 8초가 넘어간다면
            {
                _state = State.Move2Nexus;
            }

            switch (_state)
            {
                case State.Idle: //각각의 적마다 다름
                    break;
                case State.Move2Nexus:
                    toTarget(nexus.transform);
                    break;
                case State.FindPlayer:
                    toTarget(player.transform);
                    break;
            }
        }


        if (HP <= 0)
        {
            if(dieFlag == 0)
            {
                //개수 세기
                gameManager.instance.enemyKill++;
                UIController.instance.updateKillCount();
                dieFlag = 1;
                _state = State.Die;
                StartCoroutine(die());
                
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.transform.Rotate(new Vector3(0, 180f, 0));
        }

        if (collision.gameObject.CompareTag("UpgradePoint")) //베이스와 부딛히면
        {
            gameManager.instance.nexusLife--;
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            gameManager.instance.playerLife--;
            UIController.instance.updateHPBar();
            Debug.Log("<<<");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("UpgradePoint"))
        {
            //gameManager.instance.nexusLife--;
            UIController.instance.updateHPBar();
            Debug.Log("<<<");
            Destroy(gameObject);
        }

    }


}
