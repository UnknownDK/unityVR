using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class reflectiveSurfaces
{
    public static double[,] firstImageSources;
    public static double[,] secondImageSources;
    public static int[,] secondWallReflects;
    public static int[,] firstWallReflects;
    public static double[,] ISMPositions;
    public static int[,] ISMWallReflects;
    public static double[] point = {1,2,1.5} ;
    
    public static void Main() {
        //double[] point = new double{1,2,1};
        room.Room room = new room.Room();
        double[,,] wallVertices = room.GetWalls();
        
        room.WallVectors wallVectors = new room.WallVectors(wallVertices);
        double[,,] vectors = wallVectors.GetVectors();
        
        room.WallNormals wallNormals = new room.WallNormals(vectors);
        double[,] normals = wallNormals.GetNormals();
        
        FirstImageSources firstImageSource = new FirstImageSources();
        firstImageSource.FirstImageSourcesFun(point, normals, wallVertices);
        Tuple<double[,],double[,],int[,]> firstISMResults = firstImageSource.GetFirstImageSourcesAndProj();
        firstImageSources = firstISMResults.Item1;
        double[,] firstImageProjs = firstISMResults.Item2;
        firstWallReflects = firstISMResults.Item3;

        SecondImageSources secondImageSource = new SecondImageSources();
        secondImageSource.SecondImageSourcesFun(firstImageSources, normals, wallVertices, point, firstWallReflects);
        secondImageSources = secondImageSource.GetSecondImageSources();
        secondWallReflects = secondImageSource.GetSecondReflects();
    }
    public static double[,] GetISMs() {
        int y = 0;
        ISMPositions = new double[firstImageSources.GetLength(0)+secondImageSources.GetLength(0), firstImageSources.GetLength(1)];
        for (int i = 0; i < firstImageSources.GetLength(0); i++)
        {
            for (int j = 0; j < firstImageSources.GetLength(1); j++)
            {
                ISMPositions[y, j] = firstImageSources[i, j];
            }
            y++;
        }
        for (int i = 0; i < secondImageSources.GetLength(0); i++)
        {   
            for (int j = 0; j < secondImageSources.GetLength(1); j++)
            {
                ISMPositions[y, j] = secondImageSources[i, j];
            }
            y++;
        }
        return ISMPositions;
    }

    public static int[,] GetISMWallReflects()
    {
        int y = 0;
        ISMWallReflects = new int[firstWallReflects.GetLength(0)+secondWallReflects.GetLength(0),2];
        for (int i = 0; i < firstWallReflects.GetLength(0); i++)
        {
            ISMWallReflects[y,0] = firstWallReflects[i,0];
            ISMWallReflects[y,1] = firstWallReflects[i,1];
            y++;
        }
        for (int i = 0; i < secondWallReflects.GetLength(0); i++)
        {   
            ISMWallReflects[y,0] = secondWallReflects[i,0];
            ISMWallReflects[y,1] = secondWallReflects[i,1];
            y++;
        }
        return ISMWallReflects;
    }
}



