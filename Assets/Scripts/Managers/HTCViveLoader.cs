using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Reflection;
public class HTCViveLoader  {

    GameManager gm;
    PlayerLoader playLoad;
    LevelLoader levelLoad;
    GameObject HTCVive;
    GameObject steamVR;
    AudioClip audio1;

    public HTCViveLoader() { }

    public HTCViveLoader(GameManager gm, PlayerLoader playLoad)
    {
        this.gm = gm;
        this.playLoad = playLoad;
    }

    public void gripped()
    {
        if(!Microphone.IsRecording(""))
                audio1 = Microphone.Start(Microphone.devices[0], false, 6, 44100);

    }

    public void ungripped()
    {
        if (Microphone.IsRecording(""))
        {
            Microphone.End(null);
            AudioClip a = audio1;

            
            float[] samples = new float[a.samples * a.channels];
            a.GetData(samples, 0);
            string path = Path.Combine (Directory.GetCurrentDirectory(), @"TempFiles\Recordings\BaytBeautRecording.mp3");
            using (FileStream file = File.Create(path))
            {
                        utilities.ConvertAndWrite(file, a);
                        utilities.WriteHeader(file, a);
            }

			levelLoad.textOptions(path);
        }
    }

    public void setLevelLoader(LevelLoader levelLoad)
    {
        this.levelLoad = levelLoad;
    }

    public void setSteamVR(GameObject steam)
    {
        steamVR = steam;
    }

    public void setCameraRig(GameObject camera)
    {
        HTCVive = camera;
    }



}
