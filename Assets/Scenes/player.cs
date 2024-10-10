using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public float mouse;
    public GameObject bullet;
    float bulletcoll = 0;
    int flag = 0;

    float MouseX, MouseY;
    Rigidbody rb;
    // Start is called before the first frame update
    private void Rotate()
    {
        MouseX += Input.GetAxisRaw("Mouse X") * mouse* Time.deltaTime;

        MouseY -= Input.GetAxisRaw("Mouse Y") * mouse* Time.deltaTime;

        MouseY = Mathf.Clamp(MouseY, -90f, 90f); //Clamp�� ���� �ּҰ� �ִ밪�� ���� �ʵ�����

        transform.localRotation = Quaternion.Euler(MouseY, MouseX, 0f);// �� ���� �Ѳ����� ���
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) speed *= 2;
        if (Input.GetKeyUp(KeyCode.LeftShift)) speed /= 2;

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }

    }

    private void Shoot()
    {
        //��Ŭ����
        if (Input.GetKeyDown(KeyCode.Mouse0) && flag ==0)
        {
            Debug.Log("shoot bullet");
            Vector3 bulletPos = transform.position;
            bulletPos.y += 3;
            Instantiate(bullet, bulletPos, transform.rotation);
            flag = 1;
        }
        //��Ŭ����
    }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
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
            if (bulletcoll > 1)
            {
                flag = 0;
                bulletcoll = 0;
            }
        }
    }

}
