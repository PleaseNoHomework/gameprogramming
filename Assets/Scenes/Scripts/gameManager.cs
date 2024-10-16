using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class gameManager : MonoBehaviour
{

    public static gameManager instance;
    public int score = 0;
    public int enemySpawn = 10;
    public int enemyKill = 0;
    public float endTime = 5f;
    public UnityEvent gameFinished;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) { instance = this;  Debug.Log("completed gamemanager"); }
        else Debug.LogError("Duplicated gameManager", gameObject);

        

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyKill > 2)
        {
            SceneManager.LoadScene("WinScene");
        }
        endTime -= Time.deltaTime;
        Debug.Log(endTime);
        if (endTime < 0)
        {
            SceneManager.LoadScene("LoseScene");
        }

    }
}
