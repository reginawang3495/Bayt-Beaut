using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

public class HTCViveLoader
{

    GameManager gm;
    PlayerLoader playLoad;
    LevelLoader levelLoad;
    GameObject HTCVive;
    GameObject steamVR;
    AudioClip audio1;
    GameObject eye;

    public HTCViveLoader()
    {

        while (eye == null)
        {
            eye = GameObject.FindWithTag("Eye");
        }
    }

    public HTCViveLoader(GameManager gm, PlayerLoader playLoad)
    {
        this.gm = gm;
        this.playLoad = playLoad;
        while (eye == null)
        {
            eye = GameObject.FindWithTag("Eye");
        }
    }

    public void startRecording(bool grip)
    {
        if (!levelLoad.isIntro && grip)
            return;
        if (!Microphone.IsRecording(""))
            audio1 = Microphone.Start(Microphone.devices[0], false, 6, 44100);
    }

    //public  void stopRecording(bool grip)
    //{
    //    //put in monobehaviour class maybe
    //    StartCoroutine(waitForRecord(grip));

    //}

    public IEnumerator waitForRecord(bool grip)
    {
        if (Microphone.IsRecording(""))
        {
            if (!grip)
            {
                Debug.Log("real stop");
                Microphone.End(null);
                //handleRecording h = new handleRecording(levelLoad, grip, audio1);
                AudioClip a = audio1;


                    float [] samples = new float[audio1.samples];
                    audio1.GetData(samples, 0);

                    int hz = audio1.frequency;
                    int channels = audio1.channels;
                    int numSamples = audio1.samples;

                    new Task(() => { Foo(samples, hz, channels, numSamples); }).Start();
                    while (!finished)
                        yield return null;
            }
            else
                Microphone.End(null);


        }
    }

    //class handleRecording
    //{
        public bool finished = false;
        //bool grip;
        //AudioClip audio1;
        //LevelLoader levelLoad;
        //public handleRecording(LevelLoader levelLoad, bool grip, AudioClip audio1)
        //{
        //    this.levelLoad = levelLoad;
        //    this.grip = grip;
        //    this.audio1 = audio1;
        //}
        public void Foo(float [] samples, int hz, int channels, int numSamples)
        {
            Debug.Log("aerggregrgrrgargr");


            string path = Directory.GetCurrentDirectory() + "/TempFiles/Recordings/BaytBeautRecording.mp3";
                using (FileStream file = File.Create(path))
                {

                    utilities.ConvertAndWrite(file, samples);
                    utilities.WriteHeader(file, hz, channels, numSamples);
                }

                levelLoad.textOptions();
            

            Debug.Log("should be after...");

            finished = true;
            return;
        }
    //}

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

    public bool lookingAtSomething()
    {
        if (eye != null)
        {
            Vector3 fwd = eye.transform.TransformDirection(Vector3.forward);

            RaycastHit hit;
            if (Physics.Raycast(eye.transform.position, fwd, out hit, 10))
            {
                Debug.Log(hit.collider.ToString());
                return true;
            }
        }
        return false;
    }


}
