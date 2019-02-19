using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;



public class SceneLoader : MonoBehaviour{


    GameManager gm;
    PlayerLoader playLoad;
    HTCViveLoader htcLoad;
    LevelLoader levelLoad;

	public SceneLoader(){}

    public SceneLoader(GameManager gm, PlayerLoader playLoad, HTCViveLoader htcLoad)
    {
        try
        {
            this.gm = gm;
            this.playLoad = playLoad;
            this.htcLoad = htcLoad;
            SceneManager.LoadScene("Intro", LoadSceneMode.Additive);
            gm.currentScene = SceneManager.GetSceneByName("Intro");
            levelLoad.currentScene = SceneManager.GetSceneByName("Intro");
            htcLoad.setCameraRig(GameObject.FindWithTag("[CameraRig]"));
            htcLoad.setSteamVR(GameObject.FindWithTag("[SteamVR]"));
        }
        catch(Exception e)
        {
            Debug.Log(e.Message + " : "+e.StackTrace);
        }

    }

    public void LoadStart()
    {
        var asyncOp = SceneManager.LoadSceneAsync("saveme", LoadSceneMode.Additive);
        SceneManager.MoveGameObjectToScene(GameObject.FindWithTag("[CameraRig]"), SceneManager.GetSceneByName("saveme"));
        SceneManager.MoveGameObjectToScene(GameObject.FindWithTag("[SteamVR]"), SceneManager.GetSceneByName("saveme"));

        Destroy(GameObject.FindWithTag("Scene1"));

        var asynOp2 = SceneManager.UnloadSceneAsync("Intro");


    }

    public void setLevelLoader(LevelLoader levelLoad)
    {
        this.levelLoad = levelLoad;
    }

    public void LoadScene(string scene){

    }
}
