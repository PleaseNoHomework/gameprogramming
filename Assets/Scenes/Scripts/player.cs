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
    

    //����� �ҽ� 0 : �Ѱݹ� 1 : ���Ѱݹ� 2 : �������Ҹ�
    AudioSource[] audios;
    float MouseX;
    // Start is called before the first frame update
    private void Rotate()
    {
        MouseX += Input.GetAxisRaw("Mouse X") * mouse* Time.deltaTime;

        transform.localRotation = Quaternion.Euler(0f, MouseX, 0f);// �� ���� �Ѳ����� ���
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

    }

    private void Shoot()
    {
        //��Ŭ����
        if (Input.GetMouseButtonDown(0) && flag ==0)
        {
            if (gameManager.instance.bulletCharge > 0) { //�������� �ݹ�
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

        //��Ŭ����

        if (Input.GetMouseButtonDown(1))
        {
            
            if (bullet == blackBullet) //�������̶��
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Start()
    {
        audios = GetComponents<AudioSource>();
        bullet = whiteBullet;
        if (audios != null) Debug.Log("sucess auid");
    }

    // Update is called once per frame
    void Update()
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
