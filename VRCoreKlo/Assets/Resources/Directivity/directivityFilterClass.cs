using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Directivity
{
    int[] walls;
    public float azimuth = 0f;
    public float elevation = 0f;

    private float directivityScalar;

    public Directivity(int[] wallIDs)
    {
        walls = wallIDs;
    }

    public void calcDirection(float[] ISMPosition, float[] receiverPosition, float[] OGSorceDir)
    {
        float[] pointVector = new float[ISMPosition.Length];

        float distance = 0f;
        for (int i = 0; i < ISMPosition.Length; i++)
        {
            pointVector[i] = receiverPosition[i] - ISMPosition[i];
            distance += pointVector[i] * pointVector[i];
        }
        distance = Mathf.Sqrt(distance);

        azimuth = Mathf.Atan2(pointVector[1], pointVector[0]);
        azimuth += OGSorceDir[0];

        elevation = Mathf.Asin(pointVector[2]/distance);
        elevation += OGSorceDir[1];
    }

    public void calcDirectivityScalar(float[] ISMPosition, float[] OGPoint)
    {
        float[] directivityScalars = new float[2];

        if (walls[0] == -1 && walls[1] == -1)
        {
            directivityScalars[0] = (0.5f*(1f - Mathf.Cos(azimuth+Mathf.PI)) +1f)/2f;
            directivityScalars[1] = (0.5f*(1f - Mathf.Cos(elevation+Mathf.PI)) +1f)/2f;
        }
        else
        {
            if (ISMPosition[0] != OGPoint[0])
            {
                directivityScalars[0] = (0.5f*(1f - Mathf.Cos(azimuth)) +1f)/2f;
            }
            else
            {
                directivityScalars[0] = (0.5f*(1f - Mathf.Cos(azimuth+Mathf.PI)) +1f)/2f;
            }

            if(ISMPosition[2] != OGPoint[2])
            {
                directivityScalars[1] = (0.5f*(1f - Mathf.Cos(elevation+Mathf.PI)) +1f)/2f;
            }
            else
            {
                directivityScalars[1] = (0.5f*(1f - Mathf.Cos(elevation)) +1f)/2f;
            }
        }

        directivityScalar = (directivityScalars[0] + directivityScalars[1])/2f;
    }

    public void updateFilter(float[] ISMPosition, float[] receiverPosition, float[] OGSorceDir, float[] OGPoint)
    {
        calcDirection(ISMPosition, receiverPosition, OGSorceDir);
        calcDirectivityScalar(ISMPosition, OGPoint);
    }

    public float ProcessSample(float input)
    {
        return input * directivityScalar;
    }
}
