using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class HTCViveController : MonoBehaviour
{
    HTCViveLoader load;
    SteamVR_TrackedController device;
    Hand hand;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Debug.Log("hi");
            load = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().htcLoad;
            device = GetComponent<SteamVR_TrackedController>();
            device.Gripped += gripped;
            device.Ungripped += ungripped;
            device.TriggerClicked += clicked;
        }
        catch (Exception e)
        {
        }
        }

    public bool collide(Collision col, Hand h)
    {
        hand = h;
        return col.gameObject.tag == "ToPickUp" && device.triggerPressed;
    }

    void clicked(object sender, ClickedEventArgs e)
    {
        try
        {
            hand.clicked();
        }
        finally { }
    }


    void gripped(object sender, ClickedEventArgs e)
    {
        load.startRecording(true);
    }

    void ungripped(object sender, ClickedEventArgs e)
    {
        StartCoroutine(load.waitForRecord(true));
    }

}
