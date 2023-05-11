using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyFilterNoOpt {
    private static float[] internalBuffer = new float[30];
    private static float[] internalBuffer2 = new float[59];
    //private static string filename = "Filters.txt";
    
    //private static float[] twoFilters;
    //private static float[] coefficients; 
    public float applyFilter(float input, float[] coefficients) {
        float output = 0f;

        if (coefficients.Length > 30 )
        {
            for (int i = 1; i < internalBuffer2.Length; i++ )
            {
                internalBuffer2[internalBuffer2.Length-i] = internalBuffer2[internalBuffer2.Length-i-1];
            }
            internalBuffer2[0] = input;

            int n = coefficients.Length;
            int m = internalBuffer2.Length;
            for (int i = 0; i < m; i++) 
            {
                output += internalBuffer2[i]*coefficients[i];
            }
        }
        else
        {
            for (int i = 1; i < internalBuffer.Length; i++ )
            {
                internalBuffer[internalBuffer.Length-i] = internalBuffer[internalBuffer.Length-i-1];
            }
            internalBuffer[0] = input;

            int n = coefficients.Length;
            int m = internalBuffer.Length; 
            for (int i = 0; i < m; i++) 
            {
                output += internalBuffer[i]*coefficients[i];
            }
        }
        return output;
    }
}

