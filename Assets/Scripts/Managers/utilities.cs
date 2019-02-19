using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System;

using System.Net.Http;

public class utilities
{
    public static GameManager gm;
    static void Main(string[] args)
    {
        requestText("");
    }
    public static void requestText(string file)
    {
        try {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://speech.googleapis.com/v1/speech:recognize"));
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            request.Method = "POST";
            request.Headers["Authorization"] = "Bearer ya29.GluyBj6-OhrScFGDa9QyIxeGSEZl_Xzcpst6QtKkvRGbtnAX-CF7oVDsxsqFaZszHUTOfYPrtkPzaGXF1tk6F2_FWDQVT4pjQcg85lo1MFbvtJ00GcOSAwNeqBTy";
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
            if(e.Response != null)
            using (StreamReader sr = new StreamReader((e.Response as HttpWebResponse).GetResponseStream()))
            {
                string result = sr.ReadToEnd();
                Debug.Log(result);

            }

            Debug.Log(e.StackTrace);
        }
    }

}
