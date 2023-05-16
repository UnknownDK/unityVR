using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theReverbFilter : MonoBehaviour
{
    AllpassFilter filter1 = new AllpassFilter(1176, 0.7f);
    AllpassFilter filter2 = new AllpassFilter(1001, 0.7f);
    AllpassFilter filter3 = new AllpassFilter(529, 0.7f);
    AllpassFilter filter4 = new AllpassFilter(358, 0.7f);

    // Create an output signal as an array of the same length
    public float[] outputSignal = new float[256];

    // Artificial delay and gain
    //public static int predelay   = 0;
    public float pregain = 2.7368f;
    //public float[] predelayBuffer = new float[predelay + 1];
    public int predelayNewIndex = 0;
    public int predelayOldIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        filter1.Reset();
        filter2.Reset();
        filter3.Reset();
        filter4.Reset();       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for(int i = 0; i < data.Length; i+=2)
        {
            //predelayBuffer[predelayNewIndex] = pregain * ((data[i] + data[i+1])/2);
            outputSignal[i/2] = filter1.ProcessSample(pregain * ((data[i] + data[i+1])/2));
            outputSignal[i/2] = filter2.ProcessSample(outputSignal[i/2]);
            outputSignal[i/2] = filter3.ProcessSample(outputSignal[i/2]);
            outputSignal[i/2] = filter4.ProcessSample(outputSignal[i/2]);

            //predelayNewIndex = (predelayNewIndex + 1) % predelayBuffer.Length;
            //predelayOldIndex = (predelayOldIndex + 1) % predelayBuffer.Length;
        }
        for(int i = 0; i < data.Length; i+=2)
        {
            data[i] += 0.5f*outputSignal[i/2];
            data[i+1] += 0.5f*outputSignal[i/2];
        }
    }
}
