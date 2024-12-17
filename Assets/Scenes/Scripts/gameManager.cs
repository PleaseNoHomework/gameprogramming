using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class gameManager : MonoBehaviour
{

    public static gameManager instance;
    public AudioSource a;
    public GameObject realGame;
    public GameObject tutorialGame;
    public GameObject pauseGame;
    public int score = 0;
    public int enemySpawn = 40;
    public int enemyKill = 0;
    public int bulletCharge;
    public int playerLife = 3;
    public int nexusLife = 5;
    public int pauseFlag = 0;
    public int gameOverFlag = 0;
    public GameObject reloadWarning;
    public GameObject buttons;
    public bool isGamePaused = false; //게임 끝날 때 용
    public bool gamePause = false; //퍼즈 화면용
    private void Awake()
    {
        if (instance == null) { instance = this; Debug.Log("completed gamemanager"); }
        bulletCharge = 10;
    }

    private void Start()
    {
        realGame.SetActive(false);
        tutorialGame.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyKill >= 25) //25마리 이상 죽인다면
        {
            SceneManager.LoadScene("WinScene");
        }
        if ((playerLife <= 0 || nexusLife <= 0) && gameOverFlag == 0)
        {
            gameOverFlag = 1;
            StartCoroutine(gameOver());
            
        }

        if (Input.GetKeyDown(KeyCode.L)) isGamePaused = !isGamePaused;
        pressGamePause(pauseFlag);

        if (bulletCharge <= 0) reloadWarning.SetActive(true); //총알을 장전하세요!
        else reloadWarning.SetActive(false);

    }


    public void pressGameStart()
    {
        StartCoroutine(Tutorial());
    }

    public void pressGamePause(int flag)
    {
        if (Input.GetKeyDown(KeyCode.T) && flag == 1)
        {
            if (gamePause) //퍼즈 상태였다면
            {
                pauseGame.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                pauseGame.SetActive(true);
                Time.timeScale = 0f;
            }
            gamePause = !gamePause;
        }

    }

    public IEnumerator Tutorial()
    {
        buttons.SetActive(false);
        yield return FadeController.instance.fadeIn();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        tutorialGame.SetActive(false);
        realGame.SetActive(true);

        yield return FadeController.instance.fadeOut();
        a.Play();
        pauseFlag = 1;
        /// ~~~ 이후 본 게임 시작
    }

    public IEnumerator gameClear()
    {
        isGamePaused = true;

        yield return new WaitForSeconds(2f);
        yield return FadeController.instance.fadeIn();
        SceneManager.LoadScene("WinScene");
    }


    public IEnumerator gameOver()
    {
        isGamePaused = true;
        yield return FadeController.instance.fadeIn();
        //플레이어가 쓰러지는 애니메이션이나
        //기지가 부서지는 애니메이션 보여주기
        SceneManager.LoadScene("LoseScene");
    }
}
