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
            //load = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().htcLoad;
            device = GetComponent<SteamVR_TrackedController>();
            device.Gripped += gripped;
            device.Ungripped += ungripped;

        }
        catch (Exception e)
        {
        }
        }

    public void OnCollision(Collision col)
    {
        if (col.gameObject.name == "Bowl")
        {
            if (device.triggerPressed && GameObject.FindWithTag("Bowl").transform.parent != transform)
            {
                GameObject.FindWithTag("Bowl").transform.parent = transform;
                GameObject.FindWithTag("Bowl").GetComponent<Rigidbody>().useGravity = false;
            }
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
