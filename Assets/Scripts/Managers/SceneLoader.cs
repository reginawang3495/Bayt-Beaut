using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading;




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
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Intro"));
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
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Managers"));
        SceneManager.MoveGameObjectToScene(GameObject.FindWithTag("[CameraRig]"), SceneManager.GetSceneByName("Managers"));
        SceneManager.MoveGameObjectToScene(GameObject.FindWithTag("[SteamVR]"), SceneManager.GetSceneByName("Managers"));
        Destroy(GameObject.FindWithTag("Scene1"));
        SceneManager.UnloadScene("Intro");

        SceneManager.LoadSceneAsync("saveme", LoadSceneMode.Additive);


    }

    public void setLevelLoader(LevelLoader levelLoad)
    {
        this.levelLoad = levelLoad;
    }
}
