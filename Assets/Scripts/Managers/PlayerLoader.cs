using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader {

    GameManager gm;
    HTCViveLoader htcLoad;
    LevelLoader levelLoad;



    public PlayerLoader() { }

    public PlayerLoader(GameManager gm)
    {
        this.gm = gm;
    }

    public void setHTCLoader(HTCViveLoader htcLoad)
    {
        this.htcLoad = htcLoad;
    }

    public void setLevelLoader(LevelLoader levelLoad)
    {
        this.levelLoad = levelLoad;
    }
}
