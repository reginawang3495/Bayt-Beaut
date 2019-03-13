using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LeftHand : MonoBehaviour
{
    HTCViveController mother;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
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
            mother.OnCollision(col);
        }
        catch(Exception e) { }
    }
}
