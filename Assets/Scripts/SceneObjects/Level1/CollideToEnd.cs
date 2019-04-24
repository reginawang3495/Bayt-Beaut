using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class CollideToEnd : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        OnCollisionStay(col);
    }
    void OnCollisionStay(Collision col)
    {
        try
        {
            if (col.gameObject.tag == "ToPickUp")
            {
                Debug.Log("end");
                Destroy(GameObject.FindWithTag("Level1"));
                SceneManager.UnloadScene("Level1");
            }
        }
        catch (Exception e) { }
    }
}
