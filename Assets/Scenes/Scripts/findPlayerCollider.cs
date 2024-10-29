using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findPlayerCollider : MonoBehaviour
{
    public newEnemyMove enemyMove;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyMove.stopTime = 0;
            Debug.Log("Find Player!");
            enemyMove.changeState(2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyMove.stopTime = 0;
            Debug.Log("Lost Player!");
            enemyMove.changeState(3);
        }
    }
}
