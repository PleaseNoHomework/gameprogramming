using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Life : MonoBehaviour
{
    public UnityEvent onDeath;
    [SerializeField] private Life playerLife;

    private void Awake()
    {
        playerLife.onDeath.AddListener(onPlayerOrBaseDied);
    }

    private void onPlayerOrBaseDied()
    {
        SceneManager.LoadScene("WinScene");
    }

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
