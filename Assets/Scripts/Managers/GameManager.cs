using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {

    PlayerLoader playLoad;
    public HTCViveLoader htcLoad;
    public LevelLoader levelLoad;
    public SceneLoader sceneLoad;



    // Use this for initialization
    void Start () {
        try
        {
            utilities.gm = this; utilities.startup();
            playLoad = new PlayerLoader(this);
            htcLoad = new HTCViveLoader(this, playLoad);
            playLoad.setHTCLoader(htcLoad);
            sceneLoad = gameObject.AddComponent<SceneLoader>();
            sceneLoad.setStuff(this, playLoad, htcLoad);
            levelLoad = gameObject.AddComponent<IntroManager>();
            ((IntroManager)levelLoad).setStuff(this, playLoad, htcLoad); // will set level to play and htc automatically
            sceneLoad.setLevelLoader(levelLoad);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message + " : "+e.StackTrace);
        }

    }
}
