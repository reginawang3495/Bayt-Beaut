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
        new DialogElement("Dialog/Level1,Dialog2", "mom", new string[] {}, new int[] {4}),
        new DialogElement("Dialog/Level1,Dialog3", "mom", new string[] {}, new int[] {4}),
           new DialogElement("yialog/Level1,Dialog4", "dad", new string [] { }, new int [] { })
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

    public override void textOptions()
    {
        utilities.requestText(dialog[index].playerPhrases);
    }

    public IEnumerator waitSomeTime(int time)
    {
        yield return new WaitForSeconds(time);
    }

    public override void wordSaid(string word)
    {
        Debug.Log(word);
        for (int i = 0; i <= dialog[index].playerPhrases.Length; i++)
        {
            if (dialog[index].playerPhrases[i].Equals(word))
            {
                Debug.Log(index);
                index = dialog[index].nextElement[i];
            }
        }

    }


 //   [MethodImpl(MethodImplOptions.Synchronized)]

    public override IEnumerator playScene()
    {
        Debug.Log("Playing: " + Directory.GetCurrentDirectory() + "/Assets/Dialog/Level1,Dialog1");
        // check out https://answers.unity.com/questions/228150/hold-or-wait-while-coroutine-finishes.html
        while (dialog.Length > index && index != -1)
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
            textOptions();
            htcLoad.startRecording(false);


            yield return waitSomeTime(15); //TODO: put utilities in courotine and wait a little longer

            htcLoad.stopRecording(false);


            if (dialog[index].playerPhrases.Length == 0)
            {
                if (dialog[index].nextElement.Length == 0)
                    yield break; //TODO: move to next scene
                index = dialog[index].nextElement[0]; 
            }
            else
            {
                htcLoad.startRecording(false);


                yield return waitSomeTime(5); //TODO: put utilities in courotine and wait a little longer

                htcLoad.stopRecording(false);
                yield return waitSomeTime(1);

            }
            Debug.Log("continueeee");

        }
    }

}
