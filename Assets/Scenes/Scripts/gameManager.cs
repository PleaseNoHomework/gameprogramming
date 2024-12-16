using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class gameManager : MonoBehaviour
{

    public static gameManager instance;

    public GameObject realGame;
    public GameObject tutorialGame;

    public int score = 0;
    public int enemySpawn = 20;
    public int enemyKill = 0;
    public int bulletCharge;
    public int playerLife = 3;
    public int nexusLife = 3;

    public int gameOverFlag = 0;
    public GameObject reloadWarning;
    

    private void Awake()
    {
        if (instance == null) { instance = this; Debug.Log("completed gamemanager"); }
        bulletCharge = 10;
    }

    private void Start()
    {
        realGame.SetActive(false);
        tutorialGame.SetActive(true);
        StartCoroutine(Tutorial());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyKill >= 15) //15���� �̻� ���δٸ�
        {
            SceneManager.LoadScene("WinScene");
        }
        if ((playerLife <= 0 || nexusLife <= 0) && gameOverFlag == 0)
        {
            gameOverFlag = 1;
            StartCoroutine(gameOver());
            
        }

        if (bulletCharge <= 0) reloadWarning.SetActive(true); //�Ѿ��� �����ϼ���!
        else reloadWarning.SetActive(false);

    }


    public IEnumerator Tutorial()
    {
        while(true)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.A)) break;
        }

        tutorialGame.SetActive(false);
        realGame.SetActive(true);
        /// ~~~ ���� �� ���� ����
    }



    public IEnumerator gameOver()
    {
        float time = 0;
        while(time < 1f) {
            time += Time.deltaTime;
            yield return null;
        }
        //�÷��̾ �������� �ִϸ��̼��̳�
        //������ �μ����� �ִϸ��̼� �����ֱ�
        SceneManager.LoadScene("LoseScene");
    }
}
