using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class HTCViveController : MonoBehaviour
{
    HTCViveLoader load;
    SteamVR_TrackedController device;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Debug.Log("hope");
            load = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().htcLoad;
            device = GetComponent<SteamVR_TrackedController>();
            device.Gripped += gripped;
            device.Ungripped += ungripped;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message + " " + e.StackTrace);
        }
        }

    void gripped(object sender, ClickedEventArgs e)
    {
        load.gripped();
    }

    void ungripped(object sender, ClickedEventArgs e)
    {
        load.ungripped();
    }
}
