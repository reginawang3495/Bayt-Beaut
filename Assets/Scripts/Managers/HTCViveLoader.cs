using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
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
        Debug.Log("Microphone used: " + Microphone.devices[0]);
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

            using (FileStream file = File.Create("C:/Users/Regina Wang/Desktop/wwww"))
            {
                        ConvertAndWrite(file, a);
                        WriteHeader(file, a);
            }

            utilities.requestText("C:/Users/Regina Wang/Desktop/wwww");
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

    static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
    {
        var samples = new float[clip.samples];
        clip.GetData(samples, 0);
        Int16[] intData = new Int16[samples.Length];

        //converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]
        Byte[] bytesData = new Byte[samples.Length * 2];

        //bytesData array is twice the size of
        //dataSource array because a float converted in Int16 is 2 bytes.

        int rescaleFactor = 32767; //to convert float to Int16
        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * rescaleFactor);
            Byte[] byteArr = new Byte[2];
            byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, i * 2);
        }

        fileStream.Write(bytesData, 0, bytesData.Length);
    }

    static void WriteHeader(FileStream fileStream, AudioClip clip)
    {
        var hz = clip.frequency;
        var channels = clip.channels;
        var samples = clip.samples;

        fileStream.Seek(0, SeekOrigin.Begin);
        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        fileStream.Write(riff, 0, 4);

        Byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
        fileStream.Write(chunkSize, 0, 4);

        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        fileStream.Write(wave, 0, 4);

        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        fileStream.Write(fmt, 0, 4);

        Byte[] subChunk1 = BitConverter.GetBytes(16);
        fileStream.Write(subChunk1, 0, 4);

        UInt16 two = 2;
        UInt16 one = 1;

        Byte[] audioFormat = BitConverter.GetBytes(one);
        fileStream.Write(audioFormat, 0, 2);

        Byte[] numChannels = BitConverter.GetBytes(channels);
        fileStream.Write(numChannels, 0, 2);

        Byte[] sampleRate = BitConverter.GetBytes(hz);
        fileStream.Write(sampleRate, 0, 4);

        Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2
        fileStream.Write(byteRate, 0, 4);

        UInt16 blockAlign = (ushort)(channels * 2);
        fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

        UInt16 bps = 16;
        Byte[] bitsPerSample = BitConverter.GetBytes(bps);
        fileStream.Write(bitsPerSample, 0, 2);

        Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
        fileStream.Write(datastring, 0, 4);

        Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        fileStream.Write(subChunk2, 0, 4);
        //        fileStream.Close();
    }
}
