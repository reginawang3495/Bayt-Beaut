using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ChangeArmRotation : MonoBehaviour
{
    // Start is called before the first frame update
    int amtStart;
    float speed = 1f;

    void Start()
    {
        amtStart = 300;

    }

    // Update is called once per frame
    void Update()
    {
        if (amtStart > 0)
        {
            amtStart--;
            return;
        }
            Vector3 relativePos = GameObject.FindWithTag("[CameraRig]").transform.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, relativePos, speed * Time.deltaTime, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);


    }
}
