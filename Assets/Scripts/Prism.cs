﻿using UnityEngine;
using System.Collections;

public enum LightEffectType { Reflect, Refract, Block }

public class Prism : MonoBehaviour {
    public LightEffectType lightEffect; 
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void OnMouseDrag()
    {
        float deltaX = Input.GetAxis("Mouse X");
        if (deltaX < 0)
        {
            this.transform.Rotate(0, 0, deltaX * 25);
        }
        if (deltaX > 0)
        {
            this.transform.Rotate(0, 0, deltaX * 25);
        }
    }
    

    // Update is called once per frame
    void Update ()
    {
    
    }
}
