using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController instance;
    public TMP_Text killCount;
    public TMP_Text bulletCount;
    public Image bulletColor;
    public GameObject reloadUI;

    public Image currentPlayerHP;
    public Image currentNexusHP;
    public void updateKillCount()
    {
        int count = 25 - gameManager.instance.enemyKill;
        killCount.text = count.ToString();
    }

    public void updateBulletColor(int flag) //white = 1, black = 2
    {
        if (flag == 1) bulletColor.color = Color.white;
        else bulletColor.color = Color.black;
    }

    public void updateBulletRemain() //현재 남은 총알
    {
        bulletCount.text = gameManager.instance.bulletCharge.ToString();
    }

    public void updateHPBar()
    {
        currentPlayerHP.fillAmount = (float)gameManager.instance.playerLife / 3;
        currentNexusHP.fillAmount = (float)gameManager.instance.nexusLife / 3;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        updateKillCount();
        updateBulletColor(1);
        updateBulletRemain();

        reloadUI.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
    }
}
