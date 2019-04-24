using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
public class SlowlyAddToLook : MonoBehaviour
{
    // Start is called before the first frame update
    int amtStart;
    float weight;   
    void Start()
    {
        amtStart = 300;
        weight = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (amtStart > 0)
            amtStart--;
        else
        {
            if(weight < 1f)
            {
                weight += .004f;
                Component[] lookAt = GetComponents(typeof(LookAtIK));
                foreach (LookAtIK look in lookAt)
                    look.solver.IKPositionWeight = weight;
            }
            

        }
    }
}
