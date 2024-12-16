using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteEnemy : MonoBehaviour
{
    public EnemyStatus enemy;

    void whiteMove()
    {
    }


    // Start is called before the first frame update
    void Start()
    {
        enemy.setRandomDirection();
        Debug.Log("Start Walking!");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy._state == EnemyStatus.State.Idle) whiteMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WhiteBullet"))
        {
            enemy.HP--;
        }
    }

}
