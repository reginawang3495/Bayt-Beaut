using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class HTCViveController : MonoBehaviour
{
 //   HTCViveLoader load;
    SteamVR_TrackedController device;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
        //    load = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().htcLoad;
            device = GetComponent<SteamVR_TrackedController>();
        //    device.Gripped += gripped;
        //    device.Ungripped += ungripped;
            device.TriggerClicked += clicked;
            Debug.Log("clicked");

        }
        catch (Exception e)
        {
        }
        }

    void Update()
    {
        while (transform.GetComponent<Collider>().bounds.Intersects(GameObject.FindWithTag("Bowl").GetComponent<Collider>().bounds))
        {
            Debug.Log("bowl");
            GameObject.FindWithTag("Bowl").transform.parent = transform;
        }
    }

    //void gripped(object sender, ClickedEventArgs e)
    //{
    //    load.gripped();
    //}

    //void ungripped(object sender, ClickedEventArgs e)
    //{
    //    load.ungripped();
    //}

    void clicked(object sender, ClickedEventArgs e)
    {
        Debug.Log("kbleh");
        if (transform.GetComponent<Collider>().bounds.Intersects(GameObject.FindWithTag("Bowl").GetComponent<Collider>().bounds))
        {
            Debug.Log("bowl");
            GameObject.FindWithTag("Bowl").transform.parent = transform;
        }
    }

}
