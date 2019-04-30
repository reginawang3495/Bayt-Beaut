using System.IO;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading;
public class Level1Manager : LevelLoader
{


    DialogElement []dialog = { new DialogElement("Dialog/Level1,Dialog1", "mom", new string[] { "good", "bad" }, new int[] { 1, 2 }),
        new DialogElement("that's good", "mom", new string[] {}, new int[] {4}),
        new DialogElement("don't give me that attitude", "mom", new string[] {}, new int[] {4}),
           new DialogElement("your mom is right", "dad", new string [] { }, new int [] { })
    };
    AudioSource speaker1;
    AudioSource speaker2;
    int index = 0;
    int index2 = 0;
    bool started = false;

    void Start()
    {
        isIntro = false;
    }

    public void setStuff(IntroManager temp)
    {
        gm = temp.gm;
        playLoad = temp.playLoad;
        htcLoad = temp.htcLoad;

        playLoad.setLevelLoader(this);
        htcLoad.setLevelLoader(this);
        speaker1 = GameObject.FindWithTag("SpeakerSource1").GetComponent<AudioSource>();
        speaker2 = GameObject.FindWithTag("SpeakerSource2").GetComponent<AudioSource>();

    }

    void Update()
    {
        if (!started && speaker1 && speaker2 != null)
        {
            StartCoroutine(playScene());
            started = true;
        }

    }

    public override void textOptions(string file)
    {
        string[] arr = { "mirror on", "mirror off", "start" };
        utilities.requestText(file, arr);
    }

    public IEnumerator waitSomeTime(int time)
    {
        yield return new WaitForSeconds(time);
    }

    public override void wordSaid(string word)
    {
        if (word.Equals("start"))
            gm.sceneLoad.LoadStart("Level1", "Intro");

    }


 //   [MethodImpl(MethodImplOptions.Synchronized)]

    public override IEnumerator playScene()
    {
        Debug.Log("Playing: " + Directory.GetCurrentDirectory() + "/Assets/Dialog/Level1,Dialog1");
        // check out https://answers.unity.com/questions/228150/hold-or-wait-while-coroutine-finishes.html
        while (dialog.Length > index && index != -1)
        {
            Debug.Log("Playing: " + dialog[index].npcPhrase);
            AudioClip a = (AudioClip)Resources.Load("Dialog/Level1,Dialog1");
            if (dialog[index].mom)
            {
                speaker1.clip = a;
                speaker1.Play();
            }
            else
            {
                speaker2.clip = a;
                speaker2.Play();
            }
            yield return waitSomeTime(15);
            
            Debug.Log("continueeee");
            index2++;
            if (index2 > 1)
                index--;
        }
    }

}
