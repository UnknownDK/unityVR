using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HeadphonesCreateSources : MonoBehaviour
{
    public static double[,] ISMPositions;
    public static double[] originalPos;
    public static int[,] wallsReflectedOn;

    Vector3 startPos = new Vector3(1f,0f,1f);
    public static int numSource = 0;
    public static GameObject[] ISMAudioSource;
    public static GameObject originalAudioSource;

    public static int zeroOffset = 10000;

    float delayTime = 3f;
    float nextTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        reflectiveSurfaces.Main();
        ISMPositions = reflectiveSurfaces.GetISMs();
        numSource = ISMPositions.GetLength(0);
        numSource = 0;
        originalPos = reflectiveSurfaces.point;
       
        wallsReflectedOn = reflectiveSurfaces.GetISMWallReflects();     

        // load audio clip to be played and add zeros to the beginning
        AudioClip clip = Resources.Load<AudioClip>("Test Scripts/Test tones/1");
        clip = AddZerosToClip(clip, zeroOffset);
        
        // instantiate original source 
        GameObject OriginalAudio = (GameObject) Resources.Load("OriginalAudio");
        originalAudioSource = new GameObject();
        originalAudioSource = Instantiate(OriginalAudio);
        originalAudioSource.name = "original";
        originalAudioSource.transform.position = new Vector3(0f, 0f, 0f);

        originalAudioSource.GetComponent<AudioSource>().clip = clip;

        reflectiveSurfaces.point[0] = 0f;
        reflectiveSurfaces.point[2] = 0f;
        reflectiveSurfaces.point[1] = 0f;

        // instantiate ISM sources, change position and attatch sound clip
        GameObject ISMAudio = (GameObject) Resources.Load("ISMAudio"); 
        ISMAudioSource = new GameObject[numSource];
        for (int i = 0; i < numSource; i++)
        {
            ISMAudioSource[i] = Instantiate(ISMAudio);
            ISMAudioSource[i].transform.position = new Vector3(5f*i + 5f, 0f, 0f);

            clip = Resources.Load<AudioClip>("Test Scripts/Test tones/" + (i+2).ToString()); 
            clip = AddZerosToClip(clip, zeroOffset);

            ISMAudioSource[i].GetComponent<AudioSource>().clip = clip;
            ISMAudioSource[i].name = i.ToString();
        }

        nextTime = (float)AudioSettings.dspTime + 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // update position of sources each frame, moves in circles as is
        /*
        reflectiveSurfaces.point[0] = 1f+2*Math.Cos(AudioSettings.dspTime);
        reflectiveSurfaces.point[2] = 1f;
        reflectiveSurfaces.point[1] = 2f+2*Math.Sin(AudioSettings.dspTime);

        reflectiveSurfaces.Main();
        ISMPositions = reflectiveSurfaces.GetISMs();
        originalPos = reflectiveSurfaces.point;
        originalAudioSource.transform.position = new Vector3((float)originalPos[0], (float)originalPos[2], (float)originalPos[1]);
        for (int i = 0; i < numSource; i++)
        {
            ISMAudioSource[i].transform.position = new Vector3((float)ISMPositions[i,0], (float)ISMPositions[i,2], (float)ISMPositions[i,1]);
        }
        */
        // play sound if sound source is not playing
       if(nextTime < AudioSettings.dspTime && !originalAudioSource.GetComponent<AudioSource>().isPlaying)
        {
            originalAudioSource.GetComponent<AudioSource>().Play();
            for(int i = 0; i < numSource; i++)
            {
                ISMAudioSource[i].GetComponent<AudioSource>().Play();
            }
            nextTime = (float)AudioSettings.dspTime + delayTime;
        }
    }

    public void WriteString(string s)
    {
        string path = Application.persistentDataPath + "/buffer.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(s);
        writer.Close();
    }

    private AudioClip AddZerosToClip(AudioClip clip, int numZeros)
    {
        float[] data = new float[clip.samples * clip.channels];
        float[] zeros = new float[numZeros];
        float[] newData = new float[data.Length + zeros.Length];
        AudioClip result = AudioClip.Create(clip.name, newData.Length, 1, 44100, false);
        clip.GetData(data, 0);
        zeros.CopyTo(newData, 0);
        data.CopyTo(newData, zeros.Length);
        result.SetData(newData, 0);
        
        return result;
    }
}
