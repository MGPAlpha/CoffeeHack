using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeManger : Singleton<StrikeManger>
{
    public int numStrikes;
    public int maxStrikes;

    void Awake() {
        if (_instance == null) {
            InitializeSingleton();
        }
    }

    public void AddStrike()
    {
        numStrikes++;
        if (numStrikes > maxStrikes)
        {
            GameManager._instance.LoseGame();
        }
    }

}
