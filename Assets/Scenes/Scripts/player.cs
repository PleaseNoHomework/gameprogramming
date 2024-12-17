using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public float mouse;
    int bulletReload = 0;
    private GameObject bullet;
    public GameObject blackBullet;
    public GameObject whiteBullet;
    float bulletcoll = 0;
    int flag = 0;
    public Animator sn;
    public enum State
    {
        Idle,
        Walk,
        Run
    }

    public State _state;
    //오디오 소스 0 : 총격발 1 : 빈총격발 2 : 재장전소리
    AudioSource[] audios;
    float MouseX;
    // Start is called before the first frame update
    private void Rotate()
    {
        MouseX += Input.GetAxisRaw("Mouse X") * mouse* Time.deltaTime;

        transform.localRotation = Quaternion.Euler(0f, MouseX, 0f);// 각 축을 한꺼번에 계산
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) speed *= 2;
        if (Input.GetKeyUp(KeyCode.LeftShift)) speed /= 2;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }

        bool iswalk = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D); //움직이고 있는가

        if (iswalk)
        {
            sn.SetBool("walking", true);
        }
        else { 
            sn.SetBool("walking", false);
            sn.SetBool("running", false);
        }
        

        if (Input.GetKey(KeyCode.LeftShift)) //뛰고있을때
        {
            sn.SetBool("running", true);
        }
        else sn.SetBool("running", false);

        if(Input.GetKeyUp(KeyCode.LeftShift)) sn.SetBool("running", false);

    }

    private void Shoot()
    {
        //좌클릭시
        if (Input.GetMouseButtonDown(0) && flag ==0)
        {
            if (gameManager.instance.bulletCharge > 0) { //성공적인 격발
                Vector3 bulletPos = transform.position;
                bulletPos.y += 3;
                Instantiate(bullet, bulletPos, transform.rotation);
                gameManager.instance.bulletCharge--;
                UIController.instance.updateBulletRemain();
                flag = 1;
                audios[0].Play();
            }
            else
            {
                flag = 1;
                audios[1].Play(); 
            }
        }

        //우클릭시

        if (Input.GetMouseButtonDown(1))
        {
            
            if (bullet == blackBullet) //검정총이라면
            {
                bullet = whiteBullet;
                UIController.instance.updateBulletColor(1);
            }
            else
            {
                bullet = blackBullet;
                UIController.instance.updateBulletColor(2);
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletReload == 1)
        {
            Debug.Log("reload!");
            audios[2].Play();
            gameManager.instance.bulletCharge = 10;
            UIController.instance.updateBulletRemain();
        }

    }

    private void Awake()
    {

    }
    void Start()
    {
        audios = GetComponents<AudioSource>();
        bullet = whiteBullet;
        if (audios != null) Debug.Log("sucess auid");
        _state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.instance.isGamePaused)
        {
            Rotate();
            Move();
            Shoot();

            if (flag == 1)
            {
                bulletcoll += Time.deltaTime;
                if (bulletcoll > 0.5f)
                {
                    flag = 0;
                    bulletcoll = 0;
                }
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UpgradePoint")
        {
            Debug.Log("can reload!");
            UIController.instance.reloadUI.SetActive(true);
            bulletReload = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "UpgradePoint")
        {
            Debug.Log("cant reload!");
            UIController.instance.reloadUI.SetActive(false);
            bulletReload = 0;
        }
    }
}
