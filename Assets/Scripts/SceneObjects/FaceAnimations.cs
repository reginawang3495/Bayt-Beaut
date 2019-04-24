using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimations : MonoBehaviour
{
    public Texture[] faceFrames;
    public float timeStep = 0.1f;
    float timer = 0f;
    int currFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > timeStep)
        {
            currFrame = (currFrame++)%faceFrames.Length;
            GetComponent<Renderer>().material.SetTexture("_Maintex", faceFrames[currFrame]);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
