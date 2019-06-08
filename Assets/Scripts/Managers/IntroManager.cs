using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroManager : LevelLoader
{
    bool thisScene = true;
    
    public string hi()
    {
        return "hi";
    }
     void Start()
    {
        isIntro = true;
        StartCoroutine(playScene());
    }

    public void setStuff(GameManager gm, PlayerLoader playLoad, HTCViveLoader htcLoad)
    {
        this.gm = gm;
        this.playLoad = playLoad;
        this.htcLoad = htcLoad;

        playLoad.setLevelLoader(this);
        htcLoad.setLevelLoader(this);
    }

    public override bool textOptions()
    {
        string[] arr = { "mirror on", "mirror off", "start" };
        utilities.requestText(arr);
        return true;
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

    public override IEnumerator playScene()
    {
        yield return new WaitWhile(() => htcLoad == null);
        while (thisScene)
        {
            yield return new WaitWhile(() => (htcLoad.lookingAtSomething() == ""));
            htcLoad.startRecording(false);
            Debug.Log("start recording");
            int i = 0;
            for (; i < 10; i++)
            {
                yield return new WaitForSeconds(.5f); //TODO: put utilities in courotine and wait a little longer
                if (htcLoad.lookingAtSomething() == "") // break gaze then  buble fade
                    break;
            }
            Debug.Log("stop recording");

            if(i > 1)
                StartCoroutine(htcLoad.waitForRecord(false));
        }
    }
}
