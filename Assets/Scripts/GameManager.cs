using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float timer = 5 * 60;

    void Awake() {
        if (_instance == null) {
            InitializeSingleton();
        }
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
        Debug.Log("You win!");
    }

    public void LoseGame()
    {
        Debug.Log("You lose");
    }
}
