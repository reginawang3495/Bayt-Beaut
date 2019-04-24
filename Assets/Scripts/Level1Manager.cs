using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

public class Level1Manager : LevelLoader
{


    DialogElement []dialog = { new DialogElement(Path.Combine (Directory.GetCurrentDirectory(), @"Assets\Dialog\Level1,Dialog1.mp3"), new string[] { "good", "bad" }, new int[] { 1, 2 }),
        new DialogElement("that's good", new string[] {}, new int[] {}),
        new DialogElement("don't give me that attitude", new string[] {}, new int[] {       })
    };
    AudioSource speaker;
    int index2 = 0;
    bool started = false;

    public void setStuff(IntroManager temp)
    {
        gm = temp.gm;
        playLoad = temp.playLoad;
        htcLoad = temp.htcLoad;

        playLoad.setLevelLoader(this);
        htcLoad.setLevelLoader(this);
        speaker = GameObject.FindWithTag("SpeakerSource").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!started && speaker != null)
            playScene();

    }

    public override void textOptions(string file)
    {
        string[] arr = { "mirror on", "mirror off", "start" };
        utilities.requestText(file, arr);
    }

    public IEnumerator waitSomeTime(int time)
    {
        print(Time.time);
        yield return new WaitForSeconds(time);
        print(Time.time);
    }

    public override void wordSaid(string word)
    {
        if (word.Equals("start"))
            gm.sceneLoad.LoadStart("Level1", "Intro");

    }


 //   [MethodImpl(MethodImplOptions.Synchronized)]

    public override void playScene()
    {
        started = true;
        int index = 0;
        Debug.Log("Playing: " + Path.Combine(Directory.GetCurrentDirectory(), @"Assets\Dialog\Level1,Dialog1.mp3"));
        // check out https://answers.unity.com/questions/228150/hold-or-wait-while-coroutine-finishes.html
        while (dialog.Length > index && index != -1)
        {
            Debug.Log("Playing: " + Path.Combine(Directory.GetCurrentDirectory(), @"Assets\Dialog\Level1,Dialog1.mp3"));
            //AudioClip a = (AudioClip)Resources.Load(Path.Combine(Directory.GetCurrentDirectory(), @"Assets\Dialog\Level1,Dialog1.mp3"));
            //speaker.clip = a;
            //speaker.Play();
            Coroutine a = StartCoroutine(waitSomeTime(5));
            
            Debug.Log("continueeee");
            index2++;
            if (index2 > 5)
                index--;
        }
    }
}
