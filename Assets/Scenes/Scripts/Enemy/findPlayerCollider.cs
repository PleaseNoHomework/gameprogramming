using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findPlayerCollider : MonoBehaviour
{
    public EnemyStatus enemy;
    public AudioSource audios;
    public IEnumerator sheepSound()
    {
        audios.Play();
        yield return new WaitForSeconds(1.3f);
        audios.Stop();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Find!!");
            StartCoroutine(sheepSound());
            enemy._state = EnemyStatus.State.FindPlayer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy._state = EnemyStatus.State.Stop;
            enemy.stopTime = 0;
        }
    }
}
