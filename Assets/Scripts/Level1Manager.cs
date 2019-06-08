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


    DialogElement []dialog = { new DialogElement("Dialog/mom_script_0", "mom", new string[] { "coming", "what" }, new int[] { 1, 2 }), // hey nawari come over here
        new DialogElement("Dialog/mom_script_1", "mom", new string[] {"really","busy"}, new int[] {3, 4}), // make us tea?
        new DialogElement("Dialog/mom_script_2", "mom", new string[] {"yell", "coming"}, new int[] {16, 3}), // just come
           new DialogElement("Dialog/mom_script_3", "dad", new string [] {"finish" }, new int [] { 7 }), // yes! make!


        new DialogElement("Dialog/mom_script_4", "mom", new string[] {}, new int[] {5}), // funny, dont want emad make tea
        new DialogElement("Dialog/dad_script_5", "dad", new string[] {}, new int[] {6}), // your tea best
           new DialogElement("Dialog/mom_script_6", "mom", new string [] {"finish" }, new int [] { 7 }), // nawari great

        new DialogElement("Dialog/dad_script_7", "dad", new string[] {"relax"}, new int[] {8}), // so late
        new DialogElement("Dialog/dad_script_8", "dad", new string[] {}, new int[] {9}), // not safe
           new DialogElement("Dialog/mom_script_9", "mom", new string [] { "two", "AM", "last", "night", "fine" }, new int [] { 10, 10, 10, 10, 12 }), // fine

        new DialogElement("Dialog/dad_script_10", "dad", new string[] {}, new int[] {11}), // emad is boy
        new DialogElement("Dialog/mom_script_11", "mom", new string[] {"sexist", "conversation"}, new int[] {14, 15}), // girls different
           new DialogElement("Dialog/dad_script_12", "dad", new string [] {}, new int [] { 13 }), // lila art kid

        new DialogElement("Dialog/mom_script_13", "mom", new string[] {"medical", "med", "photography"}, new int[] {19, 19, 16}), // nawar not artist
        new DialogElement("Dialog/mom_script_14", "mom", new string[] {"conversation"}, new int[] {15}), // girls different
           new DialogElement("Dialog/dad_script_15", "dad", new string [] {"medical", "med", "photography"}, new int [] { 19, 19, 18 }), // lila art kid

           new DialogElement("Dialog/mom_script_16", "mom", new string [] {"coming"}, new int [] { 1 }), // lila art kid

    };
    AudioSource speaker1;
    AudioSource speaker2;
    int index = 0;
    int index2 = 0;
    bool started = false;
    int curNum;

    SpriteRenderer left;
    SpriteRenderer center;
    SpriteRenderer right;

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

        if(left == null || right == null || center == null)
        {
            left = GameObject.FindWithTag("Left").GetComponent<SpriteRenderer>();
            center = GameObject.FindWithTag("Center").GetComponent<SpriteRenderer>();
            right = GameObject.FindWithTag("Right").GetComponent<SpriteRenderer>();
        }
        checkUp();
    }

    public override bool textOptions()
    {
        utilities.requestText(dialog[index].playerPhrases);
        return true;
    }

    public IEnumerator waitSomeTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public override void wordSaid(string word)
    {
        Debug.Log(word);
        for (int i = 0; i < dialog[index].playerPhrases.Length; i++)
        {
            if (dialog[index].playerPhrases[i].Equals(word))
            {
                Debug.Log(index);
                index = dialog[index].nextElement[i];
                if (curNum == index)
                    curNum = -1;
                break;  
            }
        }

    }

    public void displayText(string image)
    {
        int difNum = 0;
        for(int i = 0; i < dialog[index].nextElement.Length; i++)
        {
            bool repeat = false;
            for (int j = 0; j < i; j++)
            {
                if (dialog[index].nextElement[i] == dialog[index].nextElement[j])
                    repeat = true;
            }
            if (!repeat)
                difNum++;
        }
            center.sprite = Resources.Load<Sprite>("ResponsesReal/"+ index);

    }

    public void eraseText()
    {
        left.sprite = null;
        center.sprite = null;
        right.sprite = null;
    }


    //   [MethodImpl(MethodImplOptions.Synchronized)]

    public override IEnumerator playScene()
    {
        Debug.Log("Playing: " + Directory.GetCurrentDirectory() + "/Assets/Dialog/Level1,Dialog1");
        // check out https://answers.unity.com/questions/228150/hold-or-wait-while-coroutine-finishes.html
        curNum = -1;
        while (dialog.Length > index && index != -1)
        {
            if (htcLoad.lookingAtSomething() != "")
                yield return new WaitWhile(() => (htcLoad.lookingAtSomething() == ""));
                if (curNum != index)
            {
                Debug.Log("Playing: " + dialog[index].npcPhrase);
                AudioClip a = (AudioClip)Resources.Load(dialog[index].npcPhrase);
                if (dialog[index].mom)
                {
                    speaker1.clip = a;
                    speaker1.Play();
                    yield return new WaitWhile(() => speaker1.isPlaying);
                }
                else
                {
                    speaker2.clip = a;
                    speaker2.Play();
                    yield return new WaitWhile(() => speaker2.isPlaying);
                }
                Debug.Log("done");
            }
            curNum = index;
            if (htcLoad.lookingAtSomething() != "") 
                yield return new WaitWhile(() => (htcLoad.lookingAtSomething() == ""));
            if (dialog[index].playerPhrases.Length == 0)
            {
                Debug.Log(":(");

                if (dialog[index].nextElement.Length == 0)
                    yield break; //TODO: move to next scene
                Debug.Log(":)");
                index = dialog[index].nextElement[0]; 
            }
            else
            {
                displayText("part" + index); // add bubble
                htcLoad.startRecording(false);
                int i = 0;
                for (; i < 10; i++)
                {
                    yield return waitSomeTime(.5f); //TODO: put utilities in courotine and wait a little longer
                    if (htcLoad.lookingAtSomething() == "") // break gaze then  buble fade
                        break;
                    if (curNum != index) {
                        StartCoroutine(htcLoad.waitForRecord(false));
                        break;
                    }
                }
                if(i > 1)
                    StartCoroutine(htcLoad.waitForRecord(false));
                eraseText();

            }
            Debug.Log("continueeee");

        }
    }

}
