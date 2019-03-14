using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LeftHand : Hand
{
    HTCViveController mother;
    GameObject inHand;
    Transform handObjParent;
    // Start is called before the first frame update
    int clicks = 100;
    void Start()
    {
    }

    void Update()
    {

        if (clicks > 0)
            clicks--;
        if (mother == null)
        {
            try
            {
                mother = GameObject.FindWithTag("L").GetComponent<HTCViveController>();
            }
            catch (Exception e) { }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        OnCollisionStay(col);
    }
    void OnCollisionStay(Collision col)
    {
        try
        {
            if (clicks == 0 && mother.collide(col, this))
            {
                col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                handObjParent = col.gameObject.transform.parent;

                col.gameObject.transform.parent = transform;
                inHand = col.gameObject;
                clicks = 100;
            }
        }
        catch (Exception e) { }
    }

    public override void clicked()
    {
        try
        {
            if (clicks > 0)
                return;
            Debug.Log("hey!");
            Debug.Log(inHand);

            inHand.GetComponent<Rigidbody>().isKinematic = false;
            inHand.transform.parent = handObjParent;
            inHand = null;
            handObjParent = null;
            clicks = 100;
        }
        catch (Exception e) { Debug.Log(e.ToString()); }
    }
}
