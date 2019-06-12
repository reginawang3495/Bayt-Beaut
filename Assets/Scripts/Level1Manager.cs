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

    static int LASTINDEX = 100;
    DialogElement []dialog = { new DialogElement("Dialog/1.a", "mom", new string[] { "coming", "need", "sure" , "what" }, new int[] { 3, 1, 3, 0 }),
        new DialogElement("Dialog/2.a", "mom", new string[] {"hear","you","yell","coming","what"}, new int[] {2,2,2,3, 1}),
        new DialogElement("Dialog/3.a", "mom", new string[] {"sure","coming", "what" }, new int[] {3, 3, 2}),
           new DialogElement("Dialog/4.a", "mom", new string[] {"tea", "now", "busy", "ask", "brother", "what" }, new int [] { 4, 4, 19, 19, 19, 3 }),
        new DialogElement("Dialog/5.a", "mom", new string[] {"project","friends", "waiting", "sweet","what"}, new int[] {5,5,5,5,4}),
        new DialogElement("Dialog/6.a", "dad", new string[] {"relax","early","late","what"}, new int[] {6,6,6,5}),

           new DialogElement("Dialog/7", "dad", new string [] {}, new int [] { 7 }),
        new DialogElement("Dialog/8.a", "mom", new string[] {"brother","late","his friends", "fine", "safe", "what"}, new int[] {8,8,8,7}),
        new DialogElement("Dialog/9", "dad", new string[] {}, new int[] {9}),
           new DialogElement("Dialog/10.a", "mom", new string [] { "ugh","wth","conversation","not now","sexist","what" }, new int [] { 10, 10, 10, 10, 10, 9 }),
        new DialogElement("Dialog/11.a", "mom", new string[] {"don't","want","talk","what"}, new int[] {11,11,11,10}),


        new DialogElement("Dialog/12.1", "dad", new string[] {"med","medical","doctor","art","what"}, new int[] {12,12,12,37, 11}), //DONE PART 1
           new DialogElement("Dialog/13", "dad", new string [] {}, new int [] { 13 }),
        new DialogElement("Dialog/14a", "mom", new string[] {"confused","what"}, new int[] {29, 13}),
        null,//null 14
        null,//null 15
        null,//null 16
        null,//null 17
        null,//null 18
        new DialogElement("Dialog/20", "mom", new string[] {}, new int[] {20}),
        new DialogElement("Dialog/21", "dad", new string[] {}, new int[] {21 }), 
        new DialogElement("Dialog/22.a", "mom", new string [] {"project","friends waiting", "sweet","what"}, new int [] {5,5,5,21}),
        null,//null 22
        null,//null 23
        null,//null 24
        new DialogElement("Dialog/26", "dad", new string[] {}, new int[] {26}),
        new DialogElement("Dialog/27.a", "mom", new string[] {"med","medical","doctor","art","what"}, new int[] {12,12,12,37, 26}),
        null, //null 28
        null, //null 29
        new DialogElement("Dialog/30", "mom", new string[] {}, new int[] {30}),
        new DialogElement("Dialog/31.a", "dad", new string[] {"meet","him","for you","i like","someone","what"}, new int[] {LASTINDEX,LASTINDEX,LASTINDEX,31,31,30  }),//MEETS IBRAHIM MED
        new DialogElement("Dialog/32", "mom", new string [] {}, new int [] {32}),
        new DialogElement("Dialog/33a", "dad", new string [] {"good","person","personality","money","not concern","what"}, new int [] {33,33,35,35,32}),

        new DialogElement("Dialog/34", "mom", new string[] {}, new int[] {34}),
        new DialogElement("Dialog/35.a", "dad", new string[] {"money","not concern","stable","job","what"}, new int[] {35,35,35,35,34}),
        new DialogElement("Dialog/36", "dad", new string [] {}, new int [] {36}),
        new DialogElement("Dialog/37a", "mom", new string [] {"nonsensical","nonsense","no","i like","someone","meet","him","what"}, new int [] { LASTINDEX,LASTINDEX,LASTINDEX,LASTINDEX,LASTINDEX,LASTINDEX,LASTINDEX,36}), 
                                                                                                                                                //NO, or MEETS IBRAHIM MED
        new DialogElement("Dialog/38a", "dad", new string[] {"morning","day","just","began","apply","after","food","what"}, new int[] {38,38,38,38,46,46,46,37}),
        new DialogElement("Dialog/39.a", "mom", new string[] {"ugh","again","disappointment","what"}, new int[] {39,39,39,38}),
        new DialogElement("Dialog/40.a", "mom", new string [] {"eating","wake up","late","die","drama","dramatic","what"}, new int [] {54,40,40,40,40,40,39}),

        new DialogElement("Dialog/41.a", "mom", new string[] {"dad","stop","nonsense","drama","death","what"}, new int[] {41,41,41,41,41,40}),
        new DialogElement("Dialog/42", "mom", new string[] {}, new int[] {42 }),
        new DialogElement("Dialog/43.a", "dad", new string [] {"help me","huh","how","i don't","need help","what"}, new int [] {43,43,43,59,59,42}),
        new DialogElement("Dialog/44.a", "mom", new string [] {"please","stop","maybe","cute","what"}, new int [] {44,44,60,60,43}),

        new DialogElement("Dialog/45", "mom", new string[] {"i","guess","what"}, new int[] {45,45,44}),
        new DialogElement("Dialog/46", "mom", new string[] {}, new int[] {LASTINDEX}),//MEETS IBRAHIM ART
        new DialogElement("Dialog/47.a", "dad", new string [] {"applying","interviews","this week","lazy","unappreciative","what"}, new int [] {47,47,47,49,49,46}),
        new DialogElement("Dialog/48.a", "mom", new string [] {"again","marriage","talk","what"}, new int [] {48,48,48,47}),
        new DialogElement("Dialog/49.a", "dad", new string [] {"not bad","will","look into","what"}, new int [] {LASTINDEX,LASTINDEX,LASTINDEX,48}), //GRAD SCHOOL ART

        new DialogElement("Dialog/50.a", "mom", new string[] {"alternatives","grad","school","graduate","what"}, new int[] {51, LASTINDEX, LASTINDEX, LASTINDEX, 49}), //GRAD SCHOOL ART
        null,//null 50
        new DialogElement("Dialog/52.a", "mom", new string [] {"ugh","don't need","help","gross","what"}, new int [] {52,52,52,52,51}),
        new DialogElement("Dialog/53.a", "mom", new string [] {"sure","yeah","i guess","what"}, new int [] { LASTINDEX, LASTINDEX, LASTINDEX, 52}), //MEETS IBRAHIM ART
        null, //null 53
        null, //null 54
        new DialogElement("Dialog/55.a", "dad", new string [] {}, new int [] {55}), //MEETS IBRAHIM ART
        new DialogElement("Dialog/49.a", "dad", new string [] {"expensive","loans","what"}, new int [] { LASTINDEX, LASTINDEX, 55 }), //MEETS IBRAHIM ART
        null, //null 57
        null, //null 58
        new DialogElement("Dialog/60.a", "dad", new string [] {"sweet","will","look into","what"}, new int [] { LASTINDEX, LASTINDEX, LASTINDEX, 59 }),
         new DialogElement("Dialog/61.a", "mom", new string [] {"okay","fine","what"}, new int [] {LASTINDEX, LASTINDEX,60}),

           new DialogElement("Dialog/mom_script_16", "mom", new string [] {"coming"}, new int [] { 1 }),

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

    public void displayText()
    {
        Debug.Log("MEMEMEMEMEMMEMEME");
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
        }//                center.sprite = Resources.Load<Sprite>("Responses/Script0");

        Debug.Log("ResponseReal/Script" + index);

        center.sprite = Resources.Load<Sprite>("Responses/Script"+ index);

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
 //           if (!htcLoad.lookingAtSomething())
 //               yield return new WaitWhile(() => !htcLoad.lookingAtSomething());
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
                curNum = index;
            }
            if (!htcLoad.lookingAtSomething()) 
                yield return new WaitWhile(() => !htcLoad.lookingAtSomething());

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

                displayText(); // add bubble
                htcLoad.startRecording(false);
                int i = 0;
                for (; i < 50; i++)
                {
                    yield return waitSomeTime(.1f); //TODO: put utilities in courotine and wait a little longer
                    
                    if (!htcLoad.lookingAtSomething()) // break gaze then  buble fade
                        break;
                    if (curNum != index) {
                        // StartCoroutine(htcLoad.waitForRecord(false));
                        i = -1;
                        break;
                    }
                }
                if (i > 5)
                    StartCoroutine(htcLoad.waitForRecord(false));
                else
                    htcLoad.waitForRecord(true);
                eraseText();

            }
            Debug.Log("continueeee");

        }
    }

}
