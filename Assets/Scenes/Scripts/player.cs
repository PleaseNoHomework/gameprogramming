using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public float mouse;
    int bulletCharge = 0;
    int bulletReload = 0;
    private GameObject bullet;
    public GameObject blueBullet;
    public GameObject redBullet;
    public GameObject greenBullet;
    float bulletcoll = 0;
    int flag = 0;

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

    }

    private void Shoot()
    {
        //좌클릭시
        if (Input.GetMouseButtonDown(0) && flag ==0 && bulletCharge > 0)
        {
            Vector3 bulletPos = transform.position;
            bulletPos.y += 3;
            Instantiate(bullet, bulletPos, transform.rotation);
            bulletCharge--;
            flag = 1;
        }
        //우클릭시

        if (Input.GetMouseButtonDown(1))
        {
            
            if (bullet == blueBullet) //blue
            {
                bullet = redBullet;
            }
            else if(bullet == redBullet)
            {
                bullet = greenBullet;
            }
            else
            {
                bullet = blueBullet;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletReload == 1)
        {
            Debug.Log("reload!");
            bulletCharge = 10;
        }

    }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Start()
    {
        bulletCharge = 10;
        bullet = blueBullet;
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
            bulletReload = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "UpgradePoint")
        {
            Debug.Log("cant reload!");
            bulletReload = 0;
        }
    }
}
