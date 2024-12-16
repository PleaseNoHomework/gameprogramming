using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemy : MonoBehaviour
{
    public EnemyStatus enemy;
    float times = 0;
    // Start is called before the first frame update

    void aiMove2()
    {
        times += Time.deltaTime;
        if (times > 0.7f)
        {
            times = 0;
            gameObject.transform.Rotate(new Vector3(0, 180f, 0));
        }
    }
    void Start()
    {
        enemy.setRandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy._state == EnemyStatus.State.Idle) aiMove2();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BlackBullet"))
        {
            enemy.HP--;
        }
    }

}
