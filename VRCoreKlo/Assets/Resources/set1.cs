using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class set1 : MonoBehaviour
{
    int len = 0;
    void OnAudioFilterRead (float[] data, int channels) 
    {
        for (int i = 0; i < data.Length; i += 1 ) 
        {
            //data[i] = 0;
            //data[i] = instTest.sampleStorage[i];
        }
        len = data.Length;
    }

    void Update()
    {
    //WriteString(len.ToString());
    }

    public static void WriteString(string s)
    {
        string path = Application.persistentDataPath + "/test.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(s);
        writer.Close();
    }
}