using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RightHand : MonoBehaviour
{
    HTCViveController mother;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if(mother == null)
        {
            try
            {
                mother = GameObject.FindWithTag("R").GetComponent<HTCViveController>();
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
        mother.OnCollision(col);
    }
}
