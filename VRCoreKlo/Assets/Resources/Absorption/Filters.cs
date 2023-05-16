using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirFilter {
    private float[] coefficients;
    private static float[][] filterCoefficients;

    public float[] FirFilterFunc(int[] wallsHit) 
    {
        FilterCoeffs.assignFilterO1131();
        filterCoefficients = FilterCoeffs.coefficients;
        if (wallsHit[1] < 0)
        {
            coefficients = filterCoefficients[wallsHit[0]];
        }
        else
        {
            coefficients = Convolve.Convo(filterCoefficients[wallsHit[0]], filterCoefficients[wallsHit[1]]);
        }
        return coefficients;
    }
}



