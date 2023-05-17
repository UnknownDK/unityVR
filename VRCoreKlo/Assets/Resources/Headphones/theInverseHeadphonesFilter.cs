using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class theReflectionFilter : MonoBehaviour
{
    private static float[][] coefficients_a = new float[2][];
    private static float[][] coefficients_b = new float[2][];
        
    
    InverseHeadphones filterL;
    InverseHeadphones filterR;

    bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        coefficients_a[0] = new float[11] { 0.8675029009f, -0.0138393547f, -0.0120388043f, -0.0093950576f, -0.0062582988f, -0.0030242605f, -0.0000706518f, 0.0022997953f, 0.0038995801f, 0.0046736748f, 0.0046923151f};
        coefficients_a[1] = new float[11] { 0.9521408257f, 0.0041871046f, 0.0039102846f, 0.0034797731f, 0.0029456352f, 0.0023664934f, 0.0018003920f, 0.0012963404f, 0.0008880273f, 0.0005907026f, 0.0004015377f};
        
        coefficients_b[0] = new float[11] { 0.8675029009f, -0.0138393547f, -0.0120388043f, -0.0093950576f, -0.0062582988f, -0.0030242605f, -0.0000706518f, 0.0022997953f, 0.0038995801f, 0.0046736748f, 0.0046923151f};
        coefficients_b[1] = new float[11] { 0.9521408257f, 0.0041871046f, 0.0039102846f, 0.0034797731f, 0.0029456352f, 0.0023664934f, 0.0018003920f, 0.0012963404f, 0.0008880273f, 0.0005907026f, 0.0004015377f};
        
        filterL = new InverseHeadphones(coefficients_a[0], coefficients_b[0]);
        filterR = new InverseHeadphones(coefficients_a[1], coefficients_b[1]);

        filterL.Reset();
        filterR.Reset();   
        ready = true;   
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if(!ready)
            return;
        for(int i = 0; i < data.Length; i+=2)
        {
            data[i]     = filterL.ProcessSample(data[i]);
            data[i+1]   = filterR.ProcessSample(data[i+1]);
        }
    }
}
