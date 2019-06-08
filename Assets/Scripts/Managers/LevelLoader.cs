using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public abstract class LevelLoader : MonoBehaviour {

    public class DialogElement
    {
        public DialogElement(string phrase, string parent, string [] phrases, int [] next) {
            npcPhrase = phrase;
            mom = parent.Equals("mom");
            playerPhrases = phrases;
            nextElement = next;
        }
        public string npcPhrase;
        public string[] playerPhrases;
        public bool mom;
        public int[] nextElement;
    }
    void Update()
    {
        checkUp();
    }

    public void checkUp()
    {
        if (word.Length != 0)
            if (count < 2)
                count++;
            else
            {
                wordSaid(word);
                count = 0;
                word = "";
            }
    }

    public string word = "";
    int count = 0;
    public bool isIntro;
    public Scene currentScene;
    public GameManager gm;
    public PlayerLoader playLoad;
    public HTCViveLoader htcLoad;
    public void setScene(Scene scene)
    {
        currentScene = scene;
    }

    public abstract bool textOptions();

    public abstract void wordSaid(string phrase);

    public abstract IEnumerator playScene();

}

