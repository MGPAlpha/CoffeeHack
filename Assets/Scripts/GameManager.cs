using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public float timer = 5 * 60;
    public float maxTime;

    void Awake()
    {
        if (_instance == null)
        {
            InitializeSingleton();
        }
        maxTime = timer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("LoseScene");
    }
}
