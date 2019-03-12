using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public abstract class LevelLoader {
    public Scene currentScene;

	public void setScene(Scene scene)
    {
        currentScene = scene;
    }

    public abstract void textOptions(string file);

    public abstract void wordSaid(string phrase);

    public abstract void playScene();
}
