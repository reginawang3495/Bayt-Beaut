using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class onRecognizePhrase {
    [SerializeField]
    LevelLoader levelLoad;
    PhraseRecognizer pr;

    public onRecognizePhrase(string [] phrases, LevelLoader levelLoad)
    {
        Debug.Log("Microphone used: " + Microphone.devices[0]);

        this.levelLoad = levelLoad;
        PhraseRecognizer pr = new KeywordRecognizer(phrases);
        this.pr = pr;
        pr.OnPhraseRecognized += OnPhraseRecognized;
        pr.Start();
    }	
	
    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        //if button pressed
        levelLoad.phraseRecognized(args.text);
        pr.Dispose();
    }
}
