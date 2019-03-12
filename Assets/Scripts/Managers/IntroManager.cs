using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroManager : LevelLoader
{

    GameManager gm;
    PlayerLoader playLoad;
    HTCViveLoader htcLoad;
    public Scene currentScene;


    public IntroManager(GameManager gm, PlayerLoader playLoad, HTCViveLoader htcLoad)
    {
        this.gm = gm;
        this.playLoad = playLoad;
        this.htcLoad = htcLoad;

        playLoad.setLevelLoader(this);
        htcLoad.setLevelLoader(this);
    }

    public override void textOptions(string file)
    {
        string[] arr = { "mirror on", "mirror off", "start" };
        utilities.requestText(file, arr);
    }

    public override void wordSaid(string word)
    {
        if (word.Equals("start"))
            gm.sceneLoad.LoadStart("Level1","Intro");
        else if (word.Equals("mirror on"))
            GameObject.FindWithTag("MagicMirror").transform.position = new Vector3(-3.4f, 1, 2);
        else if (word.Equals("mirror off"))
            GameObject.FindWithTag("MagicMirror").transform.position = new Vector3(100, -100, 100);
    }

    public override void playScene()
    {

    }
}
