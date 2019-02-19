using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : LevelLoader {

    GameManager gm;
    PlayerLoader playLoad;
    HTCViveLoader htcLoad;

    public IntroManager() { }

    public IntroManager(GameManager gm, PlayerLoader playLoad, HTCViveLoader htcLoad)
    {
        new onRecognizePhrase(new string[3] { "Start", "Begin" , ""}, this);
        this.gm = gm;
        this.playLoad = playLoad;
        this.htcLoad = htcLoad;

        playLoad.setLevelLoader(this);
        htcLoad.setLevelLoader(this);
    }

    public override void phraseRecognized(string phrase)
    {
        Debug.Log("start!");
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0.5f, 0);
    }

    public override void playScene()
    {

    }
}
