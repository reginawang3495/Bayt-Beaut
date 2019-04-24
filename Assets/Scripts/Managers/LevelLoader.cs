using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public abstract class LevelLoader : MonoBehaviour {

    public class DialogElement
    {
        public DialogElement(string phrase, string [] phrases, int [] next) {
            npcPhrase = phrase;
            playerPhrases = phrases;
            nextElement = next;
        }
        public string npcPhrase;
        public string[] playerPhrases;
        public int[] nextElement;
    }



    public Scene currentScene;
    public GameManager gm;
    public PlayerLoader playLoad;
    public HTCViveLoader htcLoad;
    public void setScene(Scene scene)
    {
        currentScene = scene;
    }

    public abstract void textOptions(string file);

    public abstract void wordSaid(string phrase);

    public abstract void playScene();

}
