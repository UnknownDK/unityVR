using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class reflectiveSurfaces
{
    public static double[,] firstImageSources;
    public static double[,] secondImageSources;
    public static string[] secondWallReflects;
    public static string[] firstWallReflects;
    public static double[,] ISMPositions;
    public static string[] ISMWallReflects;
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
        Tuple<double[,],double[,],string[]> firstISMResults = firstImageSource.GetFirstImageSourcesAndProj();
        firstImageSources = firstISMResults.Item1;
        double[,] firstImageProjs = firstISMResults.Item2;
        firstWallReflects = firstISMResults.Item3;

        Console.WriteLine("The first-order imagesources of the reflective surfaces are:");
        for (int i = 0; i < firstImageSources.GetLength(0); i++) {
            Console.WriteLine("Image {0}: ({1}, {2}, {3})", i + 1, firstImageSources[i, 0], firstImageSources[i, 1], firstImageSources[i,2]);
            Console.WriteLine("Image {0}'s wall IDs: {1}", i + 1, firstWallReflects[i]);
        }

        Console.WriteLine("Her kommer andenordens. aj-aj hr. kaptajn");
        SecondImageSources secondImageSource = new SecondImageSources();
        secondImageSource.SecondImageSourcesFun(firstImageSources, normals, wallVertices, point, firstWallReflects);
        secondImageSources = secondImageSource.GetSecondImageSources();
        secondWallReflects = secondImageSource.GetSecondReflects();
        for (int i = 0; i < secondImageSources.GetLength(0); i++) {
            Console.WriteLine("Image {0}: ({1}, {2}, {3})", i + 1, secondImageSources[i, 0], secondImageSources[i, 1], secondImageSources[i,2]);
            Console.WriteLine("Image {0}'s wall IDs: {1}", i + 1, secondWallReflects[i]);
        }

        /*
        Console.WriteLine("Her kommer tredjeordens. aj-aj hr. kaptajn");
        ThirdImageSources thirdImageSource = new ThirdImageSources(secondImageSources, firstImageProjs, point, firstImageSources);
        double[,] thirdImageSources = thirdImageSource.GetThirdImageSources();
        for (int i = 0; i < thirdImageSources.GetLength(0); i++) {
            Console.WriteLine("Image {0}: ({1}, {2}, {3})", i + 1, thirdImageSources[i, 0], thirdImageSources[i, 1], thirdImageSources[i,2]);
        }
        */
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

    public static string[] GetISMWallReflects()
    {
        int y = 0;
        ISMWallReflects = new string[firstWallReflects.GetLength(0)+secondWallReflects.GetLength(0)];
        for (int i = 0; i < firstWallReflects.GetLength(0); i++)
        {
            ISMWallReflects[y] = firstWallReflects[i];
            y++;
        }
        for (int i = 0; i < secondWallReflects.GetLength(0); i++)
        {   
            ISMWallReflects[y] = secondWallReflects[i];
            y++;
        }
        return ISMWallReflects;
    }
}



