using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Net;

using System.Net.Http;

public class utilities
{
    static string access_token;
    static string path = Directory.GetCurrentDirectory() + "/TempFiles/Config.txt";
    public static GameManager gm;
    public static bool apiRequesting = false;

    public static void startup()
    {
        Debug.Log(Directory.GetCurrentDirectory());
        access_token = System.IO.File.ReadAllLines(@path)[10];
    }

    public static string toStr(string[] arr) {
        string phrase = "[";
        for (int i = 0; i < arr.Length; i++)
            if (arr.Length - 1 != i)
                phrase += " \"" + arr[i] + "\" ,";
            else
                phrase += " \"" + arr[i] + "\"";
        phrase += "]";
        return phrase;
    }

    //static JustForCoroutines _coroutiner = null;
    //static JustForCoroutines coroutiner
    //{
    //    get
    //    {
    //        if (_coroutiner == null)
    //        {
    //            _coroutiner = new GameObject("JUST FOR COROUTINES").AddComponent<JustForCoroutines>();
    //        }
    //        return _coroutiner;
    //    }
    //}



    public static void refreshTokens()
    {
        try
        {
            string[] lines = System.IO.File.ReadAllLines(@path);
            string input = "&grant_type=refresh_token&client_id=" + lines[1] + "&refresh_token=" + lines[4] + "&client_secret=" + lines[7]; ;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://www.googleapis.com/oauth2/v4/token"));
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            using (StreamWriter stOut = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII))
                stOut.Write(input);

            HttpWebResponse hwr = (HttpWebResponse)request.GetResponse();
            using (StreamReader stRead = new StreamReader(hwr.GetResponseStream()))
            {
                string result = stRead.ReadToEnd();
                access_token = result.Substring(21, result.Substring(21).IndexOf('"'));
                lines[10] = access_token;
                System.IO.File.WriteAllLines(@path, lines);

            }
        }
        catch (WebException e)
        {
            if (e.Response != null)
                using (StreamReader sr = new StreamReader((e.Response as HttpWebResponse).GetResponseStream()))
                    Debug.Log(sr.ReadToEnd());
        }
    }


    public static void requestText(string [] phrases)
    {
        try {
            apiRequesting = true;
            string file = Directory.GetCurrentDirectory() + "/TempFiles/Recordings/BaytBeautRecording.mp3";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://speech.googleapis.com/v1/speech:recognize"));
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            request.Method = "POST";
            request.Headers["Authorization"] = "Bearer " + access_token;
            request.ContentType = "application/json";
        using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
        {
                string text = Convert.ToBase64String(File.ReadAllBytes(file));
            string json = 
                    "{" +
                    "\"config\":" +
                    "   {" +
                    "       \"languageCode\" : \"en-US\"," +
                    "\"maxAlternatives\" : 30,"+
                    "       \"speechContexts\" : [{" +
                    "           \"phrases\" : " + toStr(phrases) +
                    "       }]" +
                    "   }," +
                    "\"audio\":" +
                    "   {" +
                    "       \"content\" : \""  +text+ "\"" +
                    "   }" +
                    "}";
                Debug.Log(json);
            sw.Write(json);
        }
            HttpWebResponse hwr = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(hwr.GetResponseStream()))
            {
                string result = sr.ReadToEnd().ToLower();
                Debug.Log(result);
                for (int i = 0; i < phrases.Length; i++)
                    if (result.Contains(phrases[i]))
                    {
                        gm.levelLoad.word = (phrases[i]);
                        apiRequesting = true;
                        break;
                    }
            }
        }
        catch (WebException e)
        {
            if (e.Response != null)
                using (StreamReader sr = new StreamReader((e.Response as HttpWebResponse).GetResponseStream()))
                    Debug.Log(sr.ReadToEnd());
            refreshTokens();
            requestText(phrases);
        }

    }

    public static bool ConvertAndWrite(FileStream fileStream, float [] samples)
    {
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
        return true;
    }

    public static bool WriteHeader(FileStream fileStream, int hz, int channels, int samples)
    {
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
        return true;
    }

}


public class JustForCoroutines : MonoBehaviour
{

}