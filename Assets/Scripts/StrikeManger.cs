using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeManger : Singleton<StrikeManger>
{
    public int numStrikes;
    public int maxStrikes;

    public void AddStrike()
    {
        numStrikes++;
        if (numStrikes > maxStrikes)
        {
            GameManager._instance.LoseGame();
        }
    }

}
