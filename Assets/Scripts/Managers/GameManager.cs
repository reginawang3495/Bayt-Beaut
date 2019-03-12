using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {

    PlayerLoader playLoad;
    public HTCViveLoader htcLoad;
    public SceneLoader sceneLoad;
    public LevelLoader levelLoad;



    // Use this for initialization
    void Start () {
        try
        {
            utilities.gm = this; utilities.startup();
            playLoad = new PlayerLoader(this);
            htcLoad = new HTCViveLoader(this, playLoad);
            playLoad.setHTCLoader(htcLoad);
            sceneLoad = new SceneLoader(this, playLoad, htcLoad);
            levelLoad = new IntroManager(this, playLoad, htcLoad); // will set level to play and htc automatically
         //   utilities.requestText("");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message + " : "+e.StackTrace);
        }

    }
}
