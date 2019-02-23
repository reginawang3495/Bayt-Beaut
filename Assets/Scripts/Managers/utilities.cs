using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System;
using System.Web;

using System.Net.Http;

public class utilities
{
    static string access_token;
    static string path = "C:/Users/Regina Wang/Config.txt";
    public static GameManager gm;

    public static void startup()
    {
        access_token = System.IO.File.ReadAllLines(@path)[10];
    }

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

    public static void requestText(string file)
    {
        try {
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
                    "       \"speechContexts\" : [{" +
                    "           \"phrases\" : [\"mirror on\" , \"mirror off\" , \"start\"]" +
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
                if (result.Contains("mirror on"))
                    gm.wordSaid("mirror on");
                else if (result.Contains("mirror off"))
                    gm.wordSaid("mirror off");
                else if (result.Contains("start"))
                    gm.wordSaid("start");
                Debug.Log(result);

            }
        }
        catch (WebException e)
        {
            if (e.Response != null)
                using (StreamReader sr = new StreamReader((e.Response as HttpWebResponse).GetResponseStream()))
                    Debug.Log(sr.ReadToEnd());
            refreshTokens();
        }
    }

}
