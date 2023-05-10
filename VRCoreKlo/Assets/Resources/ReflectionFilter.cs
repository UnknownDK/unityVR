using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReflectionFilter : MonoBehaviour
{
    private static int[] walls = new int[2];
    public static float[] coefficients;
    FirFilter filter = new FirFilter();
    public static float[] samps = new float[512];
    public static float[] samps1 = new float[512];
    private static bool rdy = false;

    // Start is called before the first frame update
    void Start()
    {
        walls[0] = CreateSources.wallsReflectedOn[int.Parse(name),0];
        walls[1] = CreateSources.wallsReflectedOn[int.Parse(name),1];
        coefficients = filter.FirFilterFunc(walls);
        rdy = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if(rdy)
        {
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = ApplyFilter.applyFilter(data[i], coefficients);
            }
        }
       
    }
}
