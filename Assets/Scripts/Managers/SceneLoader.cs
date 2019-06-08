using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading;


public class SceneLoader : MonoBehaviour{


    public GameManager gm;
    public PlayerLoader playLoad;
    public HTCViveLoader htcLoad;
    public LevelLoader levelLoad;

	public SceneLoader(){}

    public void setStuff(GameManager gm, PlayerLoader playLoad, HTCViveLoader htcLoad)
    {

            this.gm = gm;
            this.playLoad = playLoad;
            this.htcLoad = htcLoad;
          SceneManager.LoadScene("Intro", LoadSceneMode.Additive);

        htcLoad.setCameraRig(GameObject.FindWithTag("[CameraRig]"));
            htcLoad.setSteamVR(GameObject.FindWithTag("[SteamVR]"));

        gm.sceneLoad.LoadStart("Level1", "Intro");

    }

    IEnumerator waitSomeTime(float x)
    {
        yield return new WaitForSeconds(x);
    }
    AsyncOperation asyncLoad;
    IEnumerator LoadLevel(string scene, string toRemove)
    {
        asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        Debug.Log("sTART Loading Scene: " + scene + "...");

        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading Scene: " + scene + "...");

            yield return null;
        }

        Debug.Log("Finished loading Scene: " + scene);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
        Scene s = SceneManager.GetSceneByName("Managers");
        Debug.Log(((IntroManager)levelLoad).hi());
        levelLoad.setScene(s);

        if (scene.Equals("Level1"))
        {
             Level1Manager newManager = gm.gameObject.AddComponent<Level1Manager>();
                  newManager.setStuff((IntroManager)levelLoad);
            levelLoad = newManager;
            gm.levelLoad = newManager;
            playLoad.setLevelLoader(newManager);
            htcLoad.setLevelLoader(newManager);
            UnityEngine.Object.DestroyImmediate(gm.gameObject.GetComponent<IntroManager>() as UnityEngine.Object, true);

        }
        Destroy(GameObject.FindWithTag(toRemove));
        SceneManager.UnloadSceneAsync(toRemove);
    }
    public void LoadStart(string toLoad, string toRemove)
    {
        StartCoroutine(LoadLevel(toLoad, toRemove));
        //Debug.Log("doneeee");
        //Destroy(GameObject.FindWithTag(toRemove));
        //SceneManager.UnloadSceneAsync(toRemove);

        //if (toLoad.Equals("Level1"))
        //{
        //    Destroy(gm.gameObject.GetComponent<LevelLoader>());
        //    Level1Manager newManager = gm.gameObject.AddComponent<Level1Manager>();
        //   newManager.setStuff((IntroManager)levelLoad);
        //    levelLoad = newManager;
        //    gm.levelLoad = newManager;
        //    playLoad.setLevelLoader(newManager);
        //    htcLoad.setLevelLoader(newManager);
        //}
        //SceneManager.MoveGameObjectToScene(GameObject.FindWithTag("[CameraRig]"), SceneManager.GetSceneByName(toLoad));
        //SceneManager.MoveGameObjectToScene(GameObject.FindWithTag("[SteamVR]"), SceneManager.GetSceneByName(toLoad));

    }

    public void setLevelLoader(LevelLoader levelLoad)
    {
        this.levelLoad = levelLoad;
        levelLoad.setScene(SceneManager.GetSceneByName("Intro"));

    }
}
