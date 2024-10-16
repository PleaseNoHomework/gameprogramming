using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public float mouse;
    private GameObject bullet;
    public GameObject blueBullet;
    public GameObject redBullet;
    public GameObject greenBullet;
    float bulletcoll = 0;
    int flag = 0;

    float MouseX, MouseY;
    Rigidbody rb;
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
        if (Input.GetMouseButtonDown(0) && flag ==0)
        {
            Debug.Log("shoot bullet");
            Vector3 bulletPos = transform.position;
            bulletPos.y += 3;
            Instantiate(bullet, bulletPos, transform.rotation);
            flag = 1;
        }
        //우클릭시

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Change bullet");
            
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
            Debug.Log(bullet.layer);
        }


    }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
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


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
    }
}
