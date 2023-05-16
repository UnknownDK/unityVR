using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReflectionFilter : MonoBehaviour
{
    private static int[] walls = new int[2];
    public static float[] coefficients;
    FirFilter filter = new FirFilter();
    ApplyFilterNoOptNew ApplyFilterLeft = new ApplyFilterNoOptNew();
    ApplyFilterNoOptNew ApplyFilterRight = new ApplyFilterNoOptNew();
    private static bool rdy = false;

    // Start is called before the first frame update
    void Start()
    {
        walls[0] = CreateSources.wallsReflectedOn[int.Parse(name),0];
        walls[1] = CreateSources.wallsReflectedOn[int.Parse(name),1];
        coefficients = filter.FirFilterFunc(walls);
        ApplyFilterLeft.setFilterLength(coefficients.Length);
        ApplyFilterRight.setFilterLength(coefficients.Length); 
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
            for(int i = 0; i < data.Length; i+=2)
            {
                data[i] = ApplyFilterLeft.applyFilter(data[i], coefficients);
                data[i+1] = ApplyFilterRight.applyFilter(data[i+1], coefficients);
            }
        }
    }
    
}
