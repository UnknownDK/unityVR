using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class recordAudio : MonoBehaviour
{
    float[] saveBuffer = new float[20000];
    int iter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(iter > 0)
        {
            string path = Application.persistentDataPath + "/" + gameObject.transform.name + "buffer.txt";
            StreamWriter writer = new StreamWriter(path, true);        
            for(int i = 0; i < saveBuffer.Length; i++)
            {
                writer.WriteLine(saveBuffer[i].ToString());
                if(saveBuffer[i] == 0f && saveBuffer[i+1] == 0f && i != saveBuffer.Length)
                {
                    i = 0;  
                    break;
                }
                saveBuffer[i] = 0f;
            }
            writer.Close();
            Debug.Log("iter" + iter.ToString());
            iter = 0;
        }
    }

    void OnAudioFilterRead (float[] data, int channels) 
    {
        for(int i = 0; i < data.Length; i++)
        {
            if(iter < 35)
            {
            saveBuffer[i + data.Length*iter] =  data[i];
            }
        }
        iter += 1;
    }
}
