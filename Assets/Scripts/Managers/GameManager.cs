using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {

    PlayerLoader playLoad;
    public HTCViveLoader htcLoad;
    SceneLoader sceneLoad;
    public Scene currentScene;
    LevelLoader levelLoad;



    // Use this for initialization
    void Start () {
        try
        {
            utilities.gm = this;
            playLoad = new PlayerLoader(this);
            htcLoad = new HTCViveLoader(this, playLoad);
            playLoad.setHTCLoader(htcLoad);
            sceneLoad = new SceneLoader(this, playLoad, htcLoad);
            currentScene = SceneManager.GetActiveScene();
            levelLoad = new IntroManager(this, playLoad, htcLoad); // will set level to play and htc automatically
         //   utilities.requestText("");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message + " : "+e.StackTrace);
        }

    }

    public void wordSaid(string word)
    {
        if (word.Equals("start"))
        {
            sceneLoad.LoadStart();
        }
        else if (word.Equals("mirror on"))
            GameObject.FindWithTag("MagicMirror").transform.position = new Vector3(-3.4f, 1, 2);
        else if(word.Equals("mirror off"))
            GameObject.FindWithTag("MagicMirror").transform.position = new Vector3(100, -100, 100);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
